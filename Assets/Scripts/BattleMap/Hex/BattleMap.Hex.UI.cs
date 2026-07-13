using Rev.Helpers.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BattleMap.Hex
{
	[DisallowMultipleComponent]
	[AddComponentMenu("BattleMap.Hex.UI")]
	public class UI : MonoBehaviour
	{

		public UIDocument UIDocument;

		public Hex Hex;

		public SimpleLabel CellLabel;

		public void Awake() => Hex ??= GetComponent<Hex>();

		public void OnEnable() {
			if (!UIDocument) return;

			var root = UIDocument.rootVisualElement;
			CellLabel = new SimpleLabel(root.Q<Label>("Hex_CellLabel"));
			CellLabel.Value = Hex.DataSO.Cell.ToString();
		}

		public void OnDisable() => CellLabel.Unbind();

	}
}