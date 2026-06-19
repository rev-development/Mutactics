using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpriteGen
{
    public class MainGen : MonoBehaviour
    {

        public GameObject PixelCubePrefab;

        public Texture2D AsepriteTexture;

        public List<PaletteRamp> Ramps = new();

        private readonly Dictionary<Vector2Int, Color32> _pixelDict = new();

        private Material GetRampMat(Color color) {
            var collatedRamps = Ramps.SelectMany(ramp => ramp.Colors)
                                     .ToDictionary(item => item.Key, item => item.Value);

            var matchKey = collatedRamps.Keys.ToList()
                                        .Find(key =>
                                                 Mathf.Approximately(key.r, color.r)
                                                 && Mathf.Approximately(key.g, color.g)
                                                 && Mathf.Approximately(key.b, color.b)
                                             );

            if (collatedRamps.ContainsKey(matchKey))
            {
                return collatedRamps[matchKey];
            }

            return null;
        }

        private Material GetBaseMat(Material material) {
            var baseMat = material;

            if (material.name == "Highlight"
                || material.name == "Shadow")
            {
                Ramps.ForEach(ramp =>
                        {
                            if (ramp.Highlight == material
                                || ramp.Shadow == material)
                            {
                                baseMat = ramp.Base;
                            }
                        }
                    );
            }

            return baseMat;
        }

        public void LoadMaterials() {
            Ramps.Clear();
            var targetFolder = "Assets/Shaders/MutacticsPalette";

            foreach (var subFolder in AssetDatabase.GetSubFolders(targetFolder))
            {
                var ramp = new PaletteRamp();

                ramp.Base = AssetDatabase.LoadAssetAtPath<Material>(subFolder + "/Base" + ".mat");

                ramp.Highlight = AssetDatabase.LoadAssetAtPath<Material>(subFolder + "/Highlight" + ".mat");

                ramp.Shadow = AssetDatabase.LoadAssetAtPath<Material>(subFolder + "/Shadow" + ".mat");

                ramp.Outline = AssetDatabase.LoadAssetAtPath<Material>(subFolder + "/Outline" + ".mat");

                Ramps.Add(ramp);
            }
        }

        public void Generate() {
            _pixelDict.Clear();

            if (AsepriteTexture != null)
            {
                // 2. Read all pixels (returns Color32 for fast performance)
                var pixels = AsepriteTexture.GetPixels();

                // 4. Or loop through all pixels in a grid
                var width = AsepriteTexture.width;
                var height = AsepriteTexture.height;

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        // Calculate flat array index based on X and Y
                        var index = y * width + x;
                        var color = pixels[index];

                        // Do something with the color
                        _pixelDict.Add(new Vector2Int(x, y), color);
                    }
                }
            }
        }

        public void ResetPixels() {
            foreach (var child in gameObject.GetComponentsInChildren<BoxCollider>())
            {
                DestroyImmediate(child.gameObject);
            }
        }

        public void GenerateTest() {
            ResetPixels();

            Generate();

            _pixelDict.ToList()
                      .ForEach(pixel =>
                               {
                                   if (pixel.Value.a == 0) return;

                                   var obj = Instantiate(PixelCubePrefab, gameObject.transform, true);
                                   obj.transform.position = new Vector3(pixel.Key.x, 0, pixel.Key.y);
                                   var newMat = GetRampMat(pixel.Value);

                                   if (newMat)
                                   {
                                       obj.GetComponent<Renderer>().material = GetBaseMat(newMat);
                                   }

                                   obj.GetComponent<UI>().Cell = new Vector3Int(pixel.Key.x, pixel.Key.y, 0);
                               }
                           );

            Debug.Log(_pixelDict.Count);
        }

        [Serializable]
        public class PaletteRamp
        {

            [SerializeField] public Material Base;

            [SerializeField] public Material Highlight;

            [SerializeField] public Material Outline;

            [SerializeField] public Material Shadow;

            public Dictionary<Color, Material> Colors =>
                new()
                {
                    { Base.color, Base },
                    { Highlight.color, Highlight },
                    { Outline.color, Outline },
                    { Shadow.color, Shadow }
                };

        }

    }
}