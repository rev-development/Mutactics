using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Core.Map.GridItem;
using Core.Map.GridItem.Composables;
using Rev.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core.Map.Manager
{
	public abstract class ManagerBase<TManager, TItem> : MonoBehaviour
		where TManager : MonoBehaviour
		where TItem : ItemBase
	{

		[SerializeField] public SerializedDictionary<Vector2Int, TItem> OccupiedCells = new();

		public TItem ActiveSelection;

		public Tilemap Tilemap;

		public UnityEvent<TItem> GridItemSelected = new();

		public abstract Options Options { get; }

		public static TManager Instance { get; private set; }

		protected virtual void Awake() {
			if (Instance != null
				&& Instance != this)
			{
				Destroy(gameObject);

				return;
			}

			Instance = this as TManager;
			DontDestroyOnLoad(gameObject);
		}

		protected virtual void Start() => GetExistingGridItems();

		protected virtual void OnDisable() => GridItemSelected.RemoveAllListeners();

		public void SelectGridItem(TItem gridItem) {
			if (ActiveSelection)
			{
				Selectable.TrySelect(ActiveSelection.gameObject, false);
			}

			if (Options.DeselectOnDoubleClick
				&& ActiveSelection == gridItem)
			{
				ActiveSelection = null;
			}

			if (gridItem)
			{
				ActiveSelection = gridItem;

				Selectable.TrySelect(ActiveSelection.gameObject, false);
			}
			else
			{
				ActiveSelection = null;
			}

			GridItemSelected.Invoke(gridItem);
		}

		public virtual void ResetMap() {
			foreach (var gridItem in gameObject.GetComponentsInChildren<TItem>())
			{
			#if UNITY_EDITOR
				gridItem.InspectorDestroy();
			#else
                Destroy(gridItem.DataSO);
                gridItem.DataSO = null;
                Destroy(gridItem.gameObject);
			#endif
			}

			if (Options.ClearTilemapOnReset)
			{
				Tilemap.ClearAllTiles();
			}

			OccupiedCells.Clear();
		}

		public void GetExistingGridItems() {
			OccupiedCells.Clear();

			foreach (var gridItem in Tilemap.GetComponentsInChildren<TItem>())
			{
				if (IsCellOccupied(gridItem.DataSOBase.Cell))
				{
					if (OccupiedCells[gridItem.Key] == null)
					{
						OccupiedCells[gridItem.Key] = gridItem;
					}
				}
				else
				{
					OccupiedCells.Add(gridItem.Key, gridItem);
				}
			}
		}

		protected bool IsCellOccupied(Vector2Int cell) => OccupiedCells.ContainsKey(cell);

		protected bool IsCellOccupied(Vector3Int cell) => OccupiedCells.ContainsKey(HexMap.GetXY(cell));

		public virtual GameObject PlaceObject(Dto itemData, GameObject objectPrefab) {
			if (IsCellOccupied(itemData.Cell)) return null;

			var objectToPlace = Instantiate(
					objectPrefab,
					Tilemap.GetCellCenterWorld(itemData.Cell),
					Quaternion.identity,
					Tilemap.gameObject.transform
				);

			if (objectToPlace.TryGetComponent(out TItem item))
			{
				item.Init(itemData, Options);
				OccupiedCells.Add(item.Key, item);
				Tilemap.SetTile(item.DataSOBase.Cell, item.DataSOBase.Tile);
			}
			else
			{
			#if UNITY_EDITOR
				DestroyImmediate(objectToPlace);
			#else
                Destroy(objectToPlace);
			#endif
			}

			return objectToPlace;
		}

		public void MoveObject(TItem item, Vector3Int from, Vector3Int to) {
			// If item is at 'from' and 'to' is empty
			if (IsCellOccupied(from)
				&& !IsCellOccupied(to))
			{
				OccupiedCells.Remove(HexMap.GetXY(from));
				OccupiedCells.Add(HexMap.GetXY(to), item);

				item.transform.position = Tilemap.GetCellCenterWorld(to);
				item.PositionChange.Invoke(to);
			}
		}

		protected List<Vector3Int> GetEmptyNeighbors() {
			List<Vector3Int> emptyNeighbors = new();

			foreach (var gridItem in OccupiedCells.Values)
			{
				emptyNeighbors.AddRange(HexMap.GetAdjacentCells(gridItem.DataSOBase.Cell));
			}

			emptyNeighbors = emptyNeighbors.Distinct()
										   .Where(cell => !OccupiedCells.Keys.Contains(new Vector2Int(cell.x, cell.y)))
										   .ToList();

			return emptyNeighbors;
		}

	}
}