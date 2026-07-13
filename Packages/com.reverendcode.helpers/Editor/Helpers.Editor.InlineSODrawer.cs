using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Rev.Helpers.Editor
{
	[CustomPropertyDrawer(typeof(InlineSOAttribute))]
	public class InlineSODrawer : PropertyDrawer
	{

		public override VisualElement CreatePropertyGUI(SerializedProperty property) {
			VisualElement root = new()
								 {
									 style =
									 {
										 marginBottom = 4,
									 },
								 };

			PropertyField objectField = new(property);
			root.Add(objectField);

			VisualElement inlineInspectorContainer = new()
													 {
														 style =
														 {
															 marginLeft = 15,
														 },
													 };

			root.Add(inlineInspectorContainer);

			RenderInlineInspector(property, inlineInspectorContainer);

			objectField.RegisterValueChangeCallback(evt =>
					{
						RenderInlineInspector(property, inlineInspectorContainer);
					}
				);

			return root;
		}

		private void RenderInlineInspector(SerializedProperty property, VisualElement container) {
			container.Clear();

			if (property.objectReferenceValue == null)
			{
				return;
			}

			SerializedObject assetSerializedObject = new(property.objectReferenceValue);

			InspectorElement inlineInspector = new(assetSerializedObject);

			container.Add(inlineInspector);
			container.Bind(assetSerializedObject);
		}

	}
}