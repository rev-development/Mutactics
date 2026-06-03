using Core.Map.GridItem;
using UnityEngine;

namespace BattleMap.Hex
{
    [CreateAssetMenu(fileName = "Hex", menuName = "Mutactics/BattleMap/Hex")]
    public class HexSO : GridItemSO<HexData, IHexData>, IHexData
    {

    }
}