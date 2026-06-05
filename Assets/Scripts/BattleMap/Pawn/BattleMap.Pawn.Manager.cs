using UnityEngine;

namespace BattleMap.Pawn
{
    [AddComponentMenu("BattleMap/Pawn/PawnManager")] [RequireComponent(typeof(Hex.Manager))]
    public class Manager : Core.Map.Manager.ManagerBase<Manager, Pawn>
    {

        public GameObject PawnPrefab;

        private BattleMap.Hex.Manager _hexManager;

        public void GenerateTestPawn() {
            var nextKey = Helpers.HexMap.GetNextAvailableKey(OccupiedCells);
            var nextCell = new Vector3Int(nextKey.x, nextKey.y, 0);

            _hexManager ??= Hex.Manager.Instance;

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