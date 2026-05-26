using Unity.Properties;
using UnityEngine.UIElements;

namespace Helpers
{
    public static class UIDataBinding
    {

        public static void BindLabel(
            UIDocument uiDocument,
            string labelName,
            object dataSource,
            string uiProperty,
            string propertyPath
        ) {
            var label = uiDocument.rootVisualElement.Q<Label>(labelName);

            if (label == null) return;

            label.dataSource = dataSource;

            label.SetBinding
                (
                    uiProperty,
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(propertyPath),
                        bindingMode = BindingMode.ToTarget
                    }
                );
        }

        public static Button GetButton(UIDocument uiDocument, string buttonName) {
            return uiDocument.rootVisualElement.Q<Button>(buttonName);

            ;
        }

    }
}