using Core.Map.GridItem;
using Core.Map.GridItem.Composables;
using UnityEngine;

namespace BattleMap.Hex
{
    [SelectionBase]
    [RequireComponent(typeof(Selectable))]
    public class Hex : Item<HexSO, HexData, IHexData>
    {

    }
}