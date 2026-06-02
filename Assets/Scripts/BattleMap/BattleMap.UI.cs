using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
#endif

namespace BattleMap
{
    [AddComponentMenu("Battle Map UI")]
    public class UI : MonoBehaviour
    {

        public Hex.Manager HexManager;

        public Pawn.Manager PawnManager;

        public UIDocument UIDocument;

        [SerializeField] [DontCreateProperty] private string _hexCell = "";

        [SerializeField] [DontCreateProperty] private string _pawnCell = "";

        public Label HexCellLabel;

        public Label PawnCellLabel;

        [CreateProperty]
        public string HexCell { get => _hexCell; set => _hexCell = value; }
        [CreateProperty]
        public string PawnCell { get => _pawnCell; set => _pawnCell = value; }

        public void OnEnable() {
            // Attach listeners to Unity Events (separate from UI binding)
            HexManager.GridItemSelected.AddListener(OnHexSelect);
            PawnManager.GridItemSelected.AddListener(OnPawnSelect);
            // Bind labels
            var root = UIDocument.rootVisualElement;
            HexCellLabel = root.Q<Label>("Hex_CellLabel");
            PawnCellLabel = root.Q<Label>("Pawn_CellLabel");


            new List<VisualElement>
            {
                HexCellLabel,
                PawnCellLabel
            }.ForEach(visualElement =>
                    {
                        if (visualElement != null)
                        {
                            visualElement.dataSource = this;
                        }
                    }
                );


            HexCellLabel?.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(HexCell)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            PawnCellLabel?.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(PawnCell)),
                        bindingMode = BindingMode.ToTarget
                    }
                );
        }

        public void OnDisable() {
            HexCellLabel.Unbind();
            PawnCellLabel.Unbind();
        }

        private void OnHexSelect(BattleMap.Hex.Hex hex) {
            HexCell = hex.DataSO.Cell.ToString();
        }

        private void OnPawnSelect(BattleMap.Pawn.Pawn pawn) {
            PawnCell = pawn.DataSO.Cell.ToString();
        }

    }
}