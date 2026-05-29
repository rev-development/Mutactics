using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.BattleMap
{
    [CustomEditor(typeof(global::BattleMap.Manager))]
    public class ManagerEditor : UnityEditor.Editor
    {

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            var battleMapManager = (global::BattleMap.Manager)target;

            var property = serializedObject.GetIterator();

            if (property.NextVisible(true))
            {
                do
                {
                    if (property.name == "MapData")
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
                        resetMapButton.RegisterCallback<ClickEvent>(evt => battleMapManager.ResetMap());


                        var generateMapButton = new Button
                        {
                            text = "Generate Map",
                            style =
                            {
                                flexGrow = 1
                            }
                        };

                        row.Add(generateMapButton);
                        generateMapButton.RegisterCallback<ClickEvent>(evt => battleMapManager.GenerateMap());


                        root.Add(row);
                    }

                    var field = new PropertyField(property);
                    root.Add(field);
                } while (property.NextVisible(false));
            }

            return root;
        }

    }
}