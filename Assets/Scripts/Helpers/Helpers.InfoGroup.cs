using System;
using Unity.Properties;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helpers
{
    [Serializable]
    public class InfoGroup
    {

        [SerializeField] [DontCreateProperty] private string _value;

        public GroupBox GroupBox;

        public Label Label;

        public InfoGroup(GroupBox groupBox, Label label) {
            GroupBox = groupBox;
            Label = label;

            if (Label == null) return;

            Label.dataSource = this;

            Label.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(Value)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            GroupBox.style.display = DisplayStyle.None;
        }

        [CreateProperty] public string Value
        {
            get => _value;
            set
            {
                _value = value;

                GroupBox.style.display = string.IsNullOrEmpty(_value) ? DisplayStyle.None : DisplayStyle.Flex;
            }
        }

        public void Unbind() {
            Label.Unbind();
        }

    }
}