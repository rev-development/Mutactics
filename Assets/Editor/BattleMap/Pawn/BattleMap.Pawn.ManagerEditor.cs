using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.BattleMap.Pawn
{
    [CustomEditor(typeof(global::BattleMap.Pawn.Manager), true)]
    public class ManagerEditor : Core.Map.ManagerEditor<global::BattleMap.Pawn.Manager, global::BattleMap.Pawn.IPawnData
        , global::BattleMap.Pawn.PawnSO, global::BattleMap.Pawn.Pawn, global::BattleMap.Pawn.PawnData>
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