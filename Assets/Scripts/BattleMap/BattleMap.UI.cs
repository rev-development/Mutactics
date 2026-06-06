using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
#endif

namespace BattleMap
{
    [AddComponentMenu("BattleMap.UI")]
    public class UI : MonoBehaviour
    {

        public Hex.Manager HexManager;

        public Pawn.Manager PawnManager;

        public UIDocument UIDocument;

        public Helpers.UI.InfoGroup HexInfoGroup;

        public Helpers.UI.InfoGroup PawnInfoGroup;

        private GroupBox _selectionPane;

        public void OnEnable() {
            // Attach listeners to Unity Events (separate from UI binding)
            HexManager.GridItemSelected.AddListener(OnHexSelect);
            PawnManager.GridItemSelected.AddListener(OnPawnSelect);
            // Bind labels
            var root = UIDocument.rootVisualElement;
            _selectionPane = root.Q<GroupBox>("SelectionPane");

            HexInfoGroup = new Helpers.UI.InfoGroup(root.Q<GroupBox>("Hex_InfoGroup"), root.Q<Label>("Hex_CellLabel"));

            PawnInfoGroup = new Helpers.UI.InfoGroup(
                    root.Q<GroupBox>("Pawn_InfoGroup"),
                    root.Q<Label>("Pawn_CellLabel")
                );

            HexInfoGroup.GroupBox.RegisterCallback<GeometryChangedEvent>(_ => UpdateSelectionPaneVisibility());
            PawnInfoGroup.GroupBox.RegisterCallback<GeometryChangedEvent>(_ => UpdateSelectionPaneVisibility());
            _selectionPane.style.display = DisplayStyle.None;
        }

        public void OnDisable() {
            HexInfoGroup.Unbind();
            PawnInfoGroup.Unbind();
        }

        private void UpdateSelectionPaneVisibility() {
            _selectionPane.style.display
                = string.IsNullOrEmpty(HexInfoGroup.Value) && string.IsNullOrEmpty(PawnInfoGroup.Value)
                    ? DisplayStyle.None
                    : DisplayStyle.Flex;
        }

        private void OnHexSelect(BattleMap.Hex.Hex hex) {
            HexInfoGroup.Value = hex ? hex.DataSO.Cell.ToString() : string.Empty;
            UpdateSelectionPaneVisibility();
        }

        private void OnPawnSelect(BattleMap.Pawn.Pawn pawn) {
            PawnInfoGroup.Value = pawn ? pawn.DataSO.Cell.ToString() : string.Empty;
            UpdateSelectionPaneVisibility();
        }

    }
}