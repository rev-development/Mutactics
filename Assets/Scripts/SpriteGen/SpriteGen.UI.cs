using TMPro;
using UnityEngine;

namespace SpriteGen
{
    [DisallowMultipleComponent]
    [AddComponentMenu("SpriteGen.UI")]
    public class UI : MonoBehaviour
    {

        // public UIDocument UIDocument;
        //
        // public Helpers.UI.SimpleLabel CellLabel;

        public Vector3Int Cell = new();
        //
        // public void OnEnable() {
        //     if (!UIDocument) return;
        //
        //     var root = UIDocument.rootVisualElement;
        //     CellLabel = new Helpers.UI.SimpleLabel(root.Q<Label>("PixelCube_CellLabel"));
        //     CellLabel.Value = Cell.ToString();
        // }

        // public void OnDisable() {
        //     CellLabel.Unbind();
        // }

        public void OnValidate() {
            var distFromLight = Mathf.FloorToInt(Vector3Int.Distance(Cell, new Vector3Int(0, 64, 0))) / 16;
            gameObject.GetComponentInChildren<TextMeshPro>().text = distFromLight.ToString();
        }

    }
}