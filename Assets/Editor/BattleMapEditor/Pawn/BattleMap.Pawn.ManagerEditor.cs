using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.BattleMapEditor.Pawn
{
    [CustomEditor(typeof(BattleMap.Pawn.Manager), true)]
    public class ManagerEditor : Core.Map.Manager.ManagerEditor<BattleMap.Pawn.Manager, BattleMap.Pawn.Pawn>
    {

        protected override List<(string, EventCallback<ClickEvent>)> GenerateAdditionalButtons() {
            var pawnManager = (global::BattleMap.Pawn.Manager)target;

            return new List<(string, EventCallback<ClickEvent>)>
            {
                ("Generate Pawn", evt => pawnManager.GenerateTestPawn())
            };
        }

    }
}