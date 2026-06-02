using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.BattleMap.Hex
{
    [CustomEditor(typeof(global::BattleMap.Hex.Manager), true)]
    public class ManagerEditor : Core.Map.ManagerEditor<global::BattleMap.Hex.Manager, global::BattleMap.Hex.IHexData,
        global::BattleMap.Hex.HexSO, global::BattleMap.Hex.Hex, global::BattleMap.Hex.HexData>
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