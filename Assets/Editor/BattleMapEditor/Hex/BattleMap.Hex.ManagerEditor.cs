using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.BattleMapEditor.Hex
{
    [CustomEditor(typeof(BattleMap.Hex.Manager), true)]
    public class ManagerEditor : Core.Map.ManagerEditor<BattleMap.Hex.Manager, BattleMap.Hex.Hex, BattleMap.Hex.HexSO,
        BattleMap.Hex.HexData, BattleMap.Hex.IHexData>
    {

        protected override List<(string, EventCallback<ClickEvent>)> GenerateAdditionalButtons() {
            var hexManager = (global::BattleMap.Hex.Manager)target;

            return new List<(string, EventCallback<ClickEvent>)>
            {
                ("Generate Hex", evt => hexManager.GenerateMap())
            };
        }

    }
}