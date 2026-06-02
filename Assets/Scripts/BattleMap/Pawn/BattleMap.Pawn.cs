using Core.Map.GridItem;
using UnityEngine;

namespace BattleMap.Pawn
{
    [SelectionBase]
    [RequireComponent(typeof(Selectable))]
    public class Pawn : GridItem<IPawnData, PawnSO>
    {

    }
}