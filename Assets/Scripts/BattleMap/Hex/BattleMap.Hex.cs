using Core.Map.GridItem;
using Core.Map.GridItem.Composables;
using UnityEngine;

namespace BattleMap.Hex
{
    [AddComponentMenu("BattleMap.Hex")]
    [DisallowMultipleComponent]
    [SelectionBase]
    [RequireComponent(typeof(Selectable))]
    public class Hex : Item<HexSO, HexData, IHexData>
    {

    }
}