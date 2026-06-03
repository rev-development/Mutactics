using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Core.Map.GridItem;
using Core.Map.GridItem.Composables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core.Map.Manager
{
    public class ManagerBase<TManager, TItem> : MonoBehaviour
        where TManager : MonoBehaviour
        where TItem : ItemBase
    {

        public UnityEvent<TItem> GridItemSelected = new();

        [SerializeField] public SerializedDictionary<Vector2Int, TItem> OccupiedCells = new();

        public TItem ActiveSelection;

        public Tilemap Tilemap;

        public Options DefaultOptions = new();

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

        protected virtual void Start() {
            GetExistingGridItems();
        }

        protected virtual void OnEnable() {
            GridItemSelected.AddListener(OnGridItemSelected);
        }

        protected virtual void OnDisable() {
            GridItemSelected.RemoveAllListeners();
        }

        protected void OnGridItemSelected(TItem gridItem) {
            if (ActiveSelection)
            {
                Selectable.TrySelect(ActiveSelection.gameObject, false);
            }


            if (DefaultOptions.DeselectOnDoubleClick
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

            if (DefaultOptions.ClearTilemapOnReset)
            {
                Tilemap.ClearAllTiles();
            }

            OccupiedCells.Clear();
        }

        public void GetExistingGridItems() {
            OccupiedCells.Clear();

            foreach (var gridItem in Tilemap.GetComponentsInChildren<TItem>())
            {
                if (OccupiedCells.ContainsKey(gridItem.DataSOBase.GetKey()))
                {
                    if (OccupiedCells[gridItem.DataSOBase.GetKey()] == null)
                    {
                        OccupiedCells[gridItem.DataSOBase.GetKey()] = gridItem;
                    }
                }
                else
                {
                    OccupiedCells.Add(gridItem.DataSOBase.GetKey(), gridItem);
                }
            }
        }

        protected bool IsCellAvailable(Vector2Int cell) {
            return Tilemap && !OccupiedCells.ContainsKey(cell);
        }

        public virtual GameObject PlaceObject(Dto itemData, GameObject objectPrefab) {
            if (!IsCellAvailable(itemData.GetKey())) return null;


            var objectToPlace = Instantiate(
                    objectPrefab,
                    Tilemap.GetCellCenterWorld(itemData.Cell),
                    Quaternion.identity,
                    Tilemap.gameObject.transform
                );

            if (objectToPlace.TryGetComponent(out TItem item))
            {
                item.Init(itemData, DefaultOptions);
                OccupiedCells.Add(item.DataSOBase.GetKey(), item);
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
            if (!OccupiedCells.ContainsKey(Helpers.HexMap.GetXY(from))) return;
            if (!IsCellAvailable(Helpers.HexMap.GetXY(to))) return;

            OccupiedCells.Remove(Helpers.HexMap.GetXY(from));
            OccupiedCells.Add(Helpers.HexMap.GetXY(to), item);

            item.transform.position = Tilemap.GetCellCenterWorld(item.DataSOBase.Cell);
            item.PositionChange.Invoke(to);
        }

        protected List<Vector3Int> GetEmptyNeighbors() {
            List<Vector3Int> emptyNeighbors = new();

            foreach (var gridItem in OccupiedCells.Values)
            {
                emptyNeighbors.AddRange(Helpers.HexMap.GetAdjacentCells(gridItem.DataSOBase.Cell));
            }

            emptyNeighbors = emptyNeighbors.Distinct()
                                           .Where(cell => !OccupiedCells.Keys.Contains(new Vector2Int(cell.x, cell.y)))
                                           .ToList();

            return emptyNeighbors;
        }

    }
}