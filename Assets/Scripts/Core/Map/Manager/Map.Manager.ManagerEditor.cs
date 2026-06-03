using System.Collections.Generic;
using Core.Map.GridItem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Core.Map.Manager
{
    public abstract class ManagerEditor<TManager, TItem> : Editor
        where TManager : ManagerBase<TManager, TItem>
        where TItem : ItemBase
    {

        private Button GenerateButton(string buttonText, EventCallback<ClickEvent> onClick) {
            var button = new Button
            {
                text = buttonText,
                style =
                {
                    flexGrow = 1
                }
            };

            button.RegisterCallback(onClick);

            return button;
        }

        protected abstract List<(string, EventCallback<ClickEvent>)> GenerateAdditionalButtons();

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            var manager = (TManager)target;

            var property = serializedObject.GetIterator();

            if (property.NextVisible(true))
            {
                do
                {
                    if (property.name == "OccupiedCells")
                    {
                        var row = new VisualElement
                        {
                            style =
                            {
                                flexDirection = FlexDirection.Row
                            }
                        };


                        row.Add(GenerateButton("Reset Map", _ => manager.ResetMap()));
                        row.Add(GenerateButton("Get Existing", _ => manager.GetExistingGridItems()));

                        GenerateAdditionalButtons()
                           .ForEach(button => row.Add(GenerateButton(button.Item1, button.Item2)));

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