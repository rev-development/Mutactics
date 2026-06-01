using Core.Map;
using UnityEngine;

namespace BattleMap.Pawn
{
    [AddComponentMenu("BattleMap/Pawn/PawnManager")] [RequireComponent(typeof(Hex.Manager))]
    public class Manager : ManagerBase<Manager, IPawnData, PawnSO, Pawn, PawnData>
    {

        public GameObject PawnPrefab;
        private BattleMap.Hex.Manager _hexManager;

        public void GenerateTestPawn() {
            var nextKey = GetNextAvailableKey();
            var nextCell = new Vector3Int(nextKey.x, nextKey.y, 0);

            _hexManager ??= (BattleMap.Hex.Manager)BattleMap.Hex.Manager.Instance;

            if (gameObject.TryGetComponent<BattleMap.Hex.Manager>(out var hexManager))
            {
                _hexManager ??= hexManager;
            }

            if (_hexManager.OccupiedCells.TryGetValue(nextKey, out var hex))
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