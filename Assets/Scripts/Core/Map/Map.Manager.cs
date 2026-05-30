using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Core.Map.GridItem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    [AddComponentMenu("Map Manager")]
    public abstract class ManagerBase<TManager, TDataInterface, TScriptableObject, TItem, TItemData> : MonoBehaviour
        where TManager : MonoBehaviour
        where TDataInterface : IGridItemData
        where TScriptableObject : GridItemSO<TDataInterface>
        where TItem : GridItem<TDataInterface, TScriptableObject>
        where TItemData : GridItemData, TDataInterface
    {

        public UnityEvent<TItem> GridItemSelected = new();
        public GridItemOptions DefaultGridItemOptions = new();
        [SerializeField] public SerializedDictionary<Vector2Int, TItem> OccupiedCells = new();
        public TItem ActiveSelection;
        public Tilemap Tilemap;
        [UsedImplicitly]
        public static ManagerBase<TManager, TDataInterface, TScriptableObject, TItem, TItemData> Instance
        {
            get;
            private set;
        }

        public virtual void Awake() {
            if (Instance != null
                && Instance != this)
            {
                Destroy(gameObject);

                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public virtual void OnEnable() {
            GridItemSelected.AddListener(OnGridItemSelected);
        }

        public virtual void OnDisable() {
            GridItemSelected.RemoveAllListeners();
        }

        public void OnGridItemSelected(TItem gridItem) {
            if (ActiveSelection)
            {
                ActiveSelection.Select.Invoke(false);
            }

            // Deselect When Clicked Again
            // if (MapItemSelected == mapItem)
            // {
            //     MapItemSelected = null;
            // }
            // else
            // {
            //     MapItemSelected = mapItem;
            //     MapItemSelected.Select.Invoke(true);
            // }

            ActiveSelection = gridItem;
            ActiveSelection.Select.Invoke(true);
        }

        public void GetExistingGridItems() {
            OccupiedCells.Clear();

            foreach (var gridItem in Tilemap.GetComponentsInChildren<TItem>())
            {
                if (IsCellAvailable(gridItem.DataSO.GetKey()))
                {
                    OccupiedCells.Add(gridItem.DataSO.GetKey(), gridItem);
                }
            }
        }

        public bool IsCellAvailable(Vector2Int cell) {
            return Tilemap && !OccupiedCells.TryGetValue(cell, out _);
        }

        public virtual GameObject PlaceObject(TItemData itemData, GameObject objectPrefab) {
            if (!IsCellAvailable(itemData.GetKey())) return null;


            var objectToPlace = Instantiate(
                    objectPrefab,
                    Tilemap.GetCellCenterWorld(itemData.Cell),
                    Quaternion.identity,
                    Tilemap.gameObject.transform
                );

            if (objectToPlace.TryGetComponent(out TItem item))
            {
                item.Init(itemData, DefaultGridItemOptions);
                OccupiedCells.Add(item.DataSO.GetKey(), item);
                Tilemap.SetTile(item.DataSO.Cell, item.DataSO.Tile);
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

        public List<Vector3Int> GetEmptyNeighbors() {
            List<Vector3Int> emptyNeighbors = new();

            foreach (var gridItem in OccupiedCells.Values)
            {
                emptyNeighbors.AddRange(Helpers.HexMap.GetAdjacentCells(gridItem.DataSO.Cell));
            }

            emptyNeighbors = emptyNeighbors.Distinct()
                                           .Where(cell => !OccupiedCells.Keys.Contains(new Vector2Int(cell.x, cell.y)))
                                           .ToList();

            return emptyNeighbors;
        }

    }
}