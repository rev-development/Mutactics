using Core.Map.GridItem;
using UnityEngine;

namespace BattleMap.Hex
{
    [SelectionBase]
    [RequireComponent(typeof(Selectable))]
    public class Hex : GridItem<IHexData, HexSO>
    {

    }
}