using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Helpers
{
    public class InfoGroup
    {

        [SerializeField] [DontCreateProperty] private string _val;

        public Label ValLabel;

        public InfoGroup(Label label) {
            ValLabel = label;

            ValLabel.dataSource = this;

            ValLabel?.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(Val)),
                        bindingMode = BindingMode.ToTarget
                    }
                );
        }

        [CreateProperty] public string Val { get => _val; set => _val = value; }

    }
}