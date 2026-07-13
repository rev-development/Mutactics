using Core.Map.Manager;
using Rev.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleMap.Pawn
{
	[AddComponentMenu("BattleMap.Pawn.Manager")] [RequireComponent(typeof(Hex.Manager))]
	public class Manager : ManagerBase<Manager, Pawn>
	{

		public GameObject PawnPrefab;

		[FormerlySerializedAs("_hexManager")] public BattleMap.Hex.Manager HexManager;

		public override Options Options { get; } = new();

		public void GenerateTestPawn() {
			var nextKey = HexMap.GetNextAvailableKey(OccupiedCells);
			var nextCell = new Vector3Int(nextKey.x, nextKey.y, 0);

			HexManager ??= Hex.Manager.Instance;

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
							   Tile = null,
						   };

			PlaceObject(pawnData, PawnPrefab);
		}

	}
}