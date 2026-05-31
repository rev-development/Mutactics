using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.BattleMap.Pawn
{
    [CustomEditor(typeof(global::BattleMap.Pawn.Manager), true)]
    public class ManagerEditor : UnityEditor.Editor
    {

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            var pawnManager = (global::BattleMap.Pawn.Manager)target;

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
                        resetMapButton.RegisterCallback<ClickEvent>(evt => pawnManager.ResetMap());

                        var generatePawnButton = new Button
                        {
                            text = "Generate Pawn",
                            style =
                            {
                                flexGrow = 1
                            }
                        };

                        row.Add(generatePawnButton);
                        generatePawnButton.RegisterCallback<ClickEvent>(evt => pawnManager.GenerateTestPawn());

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