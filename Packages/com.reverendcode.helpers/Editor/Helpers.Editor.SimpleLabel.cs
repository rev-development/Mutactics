using System;
using Unity.Properties;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rev.Helpers.Editor
{
	[Serializable]
	public class SimpleLabel
	{

		[SerializeField] [DontCreateProperty] private string _value;

		public Label Label;

		public SimpleLabel(Label label) {
			Label = label;

			if (Label == null) return;

			Label.dataSource = this;

			Label.SetBinding(
					"text",
					new DataBinding
					{
						dataSourcePath = new PropertyPath(nameof(Value)),
						bindingMode = BindingMode.ToTarget,
					}
				);
		}

		[CreateProperty] public string Value { get => _value; set => _value = value; }

		public void Unbind() => Label.Unbind();

	}
}