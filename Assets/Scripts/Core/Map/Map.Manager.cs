using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    [AddComponentMenu("Map Manager")]
    public abstract class Manager<T0, T1> : MonoBehaviour where T0 : MonoBehaviour where T1 : GridItem
    {

        #region Events

        public UnityEvent<T1> GridItemSelected = new();

        #endregion

        public void OnGridItemSelected(T1 gridItem) {
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

            foreach (var gridItem in Tilemap.GetComponentsInChildren<T1>())
            {
                if (IsCellAvailable(gridItem.Cell))
                {
                    OccupiedCells.Add(gridItem.Cell, gridItem);
                }
            }
        }

        public bool IsCellAvailable(Vector3Int hexCords) {
            return !OccupiedCells.TryGetValue(hexCords, out _);
        }

        public virtual GameObject PlaceObject(Vector3Int hexCords, GameObject objectPrefab) {
            if (!IsCellAvailable(hexCords)) return null;

            var objectToPlace = Instantiate
                (
                    objectPrefab,
                    Tilemap.CellToWorld(hexCords),
                    Quaternion.identity,
                    Tilemap.gameObject.transform
                );

            if (objectToPlace.TryGetComponent(out T1 gridItem))
            {
                OccupiedCells.Add(hexCords, gridItem);

                gridItem.AssignCords(hexCords, Tilemap.GetTile(hexCords));
            }
            else
            {
                Destroy(objectToPlace);
            }

            return objectToPlace;
        }

        public List<Vector3Int> GetEmptyNeighbors() {
            List<Vector3Int> emptyNeighbors = new();

            foreach (var occupiedCell in OccupiedCells.Keys)
            {
                emptyNeighbors.AddRange(Helpers.HexMap.GetAdjacentCells(occupiedCell));
            }

            emptyNeighbors = emptyNeighbors.Distinct().Where(cell => !OccupiedCells.Keys.Contains(cell)).ToList();

            return emptyNeighbors;
        }

        #region Lifecycle

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

        #endregion

        #region Runtime Values

        [SerializeField] public SerializedDictionary<Vector3Int, T1> OccupiedCells = new();
        public T1 ActiveSelection;

        #endregion

        #region References

        [Header("References")]
        public Tilemap Tilemap;
        public Tile SimpleColorHex;
        public static Manager<T0, T1> Instance { get; private set; }

        #endregion

    }
}