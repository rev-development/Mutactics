using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rev.Helpers
{
	public static class HexMap
	{

		public static List<Vector3Int> AdjacencyMatrix = new()
														 {
															 new Vector3Int(-1, 0, 0),
															 new Vector3Int(1, 0, 0),
															 new Vector3Int(0, 1, 0),
															 new Vector3Int(-1, 1, 0),
															 new Vector3Int(0, -1, 0),
															 new Vector3Int(-1, -1, 0),
														 };

		public static List<Vector3Int> GetAdjacentCells(Vector3Int hexCords) {
			var adjacentCells = new List<Vector3Int>();

			AdjacencyMatrix.ForEach(adjVec => adjacentCells.Add(adjVec + hexCords));

			return adjacentCells;
		}

		public static List<Vector2Int> GetEdgeCells(List<Vector2Int> cells, Vector2Int mapSize) =>
			cells.Where(cell => cell.x == 0 || cell.x == mapSize.x - 1 || cell.y == 0 || cell.y == mapSize.y - 1)
				 .ToList();

		public static List<Vector2Int> GetInteriorCells(List<Vector2Int> cells, Vector2Int mapSize) =>
			cells.Where(cell => cell.x != 0 && cell.x != mapSize.x - 1 && cell.y != 0 && cell.y != mapSize.y - 1)
				 .ToList();

		public static Vector2Int GetNextAvailableKey<TValue>(Dictionary<Vector2Int, TValue> dictionary) {
			var nextKey = new Vector2Int();

			while (dictionary.ContainsKey(nextKey))
			{
				if (nextKey.x > nextKey.y)
				{
					nextKey.y += 1;
				}
				else
				{
					nextKey.x += 1;
				}
			}

			return nextKey;
		}

		public static Vector2Int GetXY(Vector3Int vector3Int) => new(vector3Int.x, vector3Int.y);

	}
}