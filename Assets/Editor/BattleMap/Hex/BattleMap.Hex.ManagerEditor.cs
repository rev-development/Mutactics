using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.BattleMap.Hex
{
    [CustomEditor(typeof(global::BattleMap.Hex.Manager), true)]
    public class ManagerEditor : UnityEditor.Editor
    {

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            var hexManager = (global::BattleMap.Hex.Manager)target;

            var property = serializedObject.GetIterator();

            if (property.NextVisible(true))
            {
                do
                {
                    if (property.name == "OccupiedCells")
                    {
                        var row = new VisualElement();
                        row.style.flexDirection = FlexDirection.Row;

                        var resetMapButton = new Button
                        {
                            text = "Reset Map",
                            style =
                            {
                                flexGrow = 1
                            }
                        };

                        row.Add(resetMapButton);
                        resetMapButton.RegisterCallback<ClickEvent>(evt => hexManager.ResetMap());


                        var generateMapButton = new Button
                        {
                            text = "Generate Map",
                            style =
                            {
                                flexGrow = 1
                            }
                        };

                        row.Add(generateMapButton);
                        generateMapButton.RegisterCallback<ClickEvent>(evt => hexManager.GenerateMap());


                        root.Add(row);
                    }

                    var field = new PropertyField(property);
                    root.Add(field);
                } while (property.NextVisible(false));
            }

            root.Bind(serializedObject);

            return root;
        }

    }
}