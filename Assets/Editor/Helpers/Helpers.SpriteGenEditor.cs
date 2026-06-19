using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Helpers
{
    [CustomEditor(typeof(SpriteGen.MainGen))]
    public class SpriteGenEditor : UnityEditor.Editor
    {

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();
            var gen = (SpriteGen.MainGen)target;

            var genButton = new Button
                            {
                                text = "Generate",
                                style =
                                {
                                    flexGrow = 1
                                }
                            };

            genButton.RegisterCallback<ClickEvent>(evt => gen.GenerateTest());

            var matsButton = new Button
                             {
                                 text = "Load Mats",
                                 style =
                                 {
                                     flexGrow = 1
                                 }
                             };

            matsButton.RegisterCallback<ClickEvent>(evt => gen.LoadMaterials());

            var resetButton = new Button
                              {
                                  text = "Reset Pixels",
                                  style =
                                  {
                                      flexGrow = 1
                                  }
                              };

            resetButton.RegisterCallback<ClickEvent>(evt => gen.ResetPixels());

            root.Add(genButton);
            root.Add(matsButton);
            root.Add(resetButton);
            InspectorElement.FillDefaultInspector(root, new SerializedObject(gen), this);

            return root;
        }

    }
}