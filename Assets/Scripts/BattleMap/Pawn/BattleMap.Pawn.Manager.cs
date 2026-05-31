using Core.Map;
using Core.Map.GridItem;
using UnityEngine;

namespace BattleMap.Pawn
{
    [AddComponentMenu("Battle Map Pawn Manager")] [RequireComponent(typeof(Hex.Manager))]
    public class Manager : ManagerBase<Manager, IPawnData, PawnSO, Pawn, PawnData>
    {

        public GridItemOptions GridItemOptions = new()
        {
            OffsetProBuilderMesh = false
        };
        public GameObject PawnPrefab;
        public BattleMap.Hex.Manager HexManager;

        public void GenerateTestPawn() {
            var nextKey = GetNextAvailableKey();
            var nextCell = new Vector3Int(nextKey.x, nextKey.y, 0);

            HexManager ??= (BattleMap.Hex.Manager)BattleMap.Hex.Manager.Instance;

            if (gameObject.TryGetComponent<BattleMap.Hex.Manager>(out var hexManager))
            {
                HexManager ??= hexManager;
            }

            if (HexManager.OccupiedCells.TryGetValue(nextKey, out var hex))
            {
                if (hex.DataSO)
                {
                    nextCell.z = hex.DataSO.Cell.z;
                }
            }

            var pawnData = new PawnData
            {
                Cell = nextCell,
                Tile = null
            };


            PlaceObject(pawnData, PawnPrefab);
        }

    }
}