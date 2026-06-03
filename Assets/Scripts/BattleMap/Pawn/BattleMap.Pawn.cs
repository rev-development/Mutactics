using Core.Map.GridItem;
using Core.Map.GridItem.Composables;
using UnityEngine;

namespace BattleMap.Pawn
{
    [SelectionBase]
    [RequireComponent(typeof(Selectable))]
    public class Pawn : Item<PawnSO, PawnData, IPawnData>
    {

    }
}