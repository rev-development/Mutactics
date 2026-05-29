using System.Linq;
using CampaignMap;
using Core.Map;
using UnityEngine;

namespace BattleMap
{
    [AddComponentMenu("Battle Map Manager")]
    public class Manager : Manager<Manager, Hex>
    {

        public WorldData WorldData;
        public CampaignMap.Manager CampaignMapManager;
        public MapData MapData = new();

        private bool PreMapGenCheck() {
            if (!CampaignMapManager)
            {
                CampaignMapManager = CampaignMap.Manager.Instance as CampaignMap.Manager;
            }

            if (CampaignMapManager
                && CampaignMapManager.ActiveSelection
                && CampaignMapManager.ActiveSelection.TryGetComponent<WorldData>(out var worldData))
            {
                WorldData = worldData;
            }

            return WorldData && Tilemap;
        }

        public void ResetMap() {
            foreach (var gridItem in MapData.Values.Where(gridItem => gridItem.HexSpacer))
            {
            #if UNITY_EDITOR
                DestroyImmediate(gridItem.HexSpacer);
            #else
                Destroy(gridItem.HexSpacer);
            #endif
            }

            MapData.Clear();

            Tilemap.ClearAllTiles();

            OccupiedCells.Clear();
        }

        public void GenerateMap() {
            if (!PreMapGenCheck()) return;

            ResetMap();

            MapData = MapGen.GenerateMapData(WorldData, SimpleColorHex, HexSpacer);

            foreach (var cellData in MapData)
            {
                Tilemap.SetTile(cellData.Value.Cell, cellData.Value.Tile);
                var randomColor = Random.ColorHSV();
                Tilemap.SetColor(cellData.Value.Cell, randomColor);

                var hex = PlaceObject(cellData.Value.Cell, HexSpacer);
                cellData.Value.HexSpacer = hex;

                if (hex)
                {
                    var hexScale = hex.transform.localScale;
                    hexScale.y = cellData.Value.Cell.z;
                    hex.transform.localScale = hexScale;

                    // Offset for Tilemap Alignment
                    if (hex.TryGetComponent<MeshFilter>(out var proBuilderMesh))
                    {
                        var hexPos = hex.transform.position;

                        hexPos.y -= proBuilderMesh.sharedMesh.bounds.size.y * hex.transform.localScale.y / 2;

                        hex.transform.position = hexPos;
                    }

                    // TODO: Set Material
                    // Don't modify renderer.material from editor (the random color requires new material generated)
                    // Helpers.Shaders.ChangeSimpleColor(randomColor, hex);
                }
            }
        }

    }
}