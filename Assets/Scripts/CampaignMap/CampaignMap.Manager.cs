using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace CampaignMap
{
    public class Manager : MonoBehaviour
    {

        public static List<Vector3Int> AdjacencyMatrix = new()
        {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(-1, -1, 0)
        };

        public Tilemap Tilemap;

        public GameObject WorldPrefab;

        [SerializeField] public SerializedDictionary<Vector3Int, MapItem> OccupiedCells = new();

        public UnityEvent<MapItem> MapItemSelect = new();

        public MapItem MapItemSelected { get; private set; }

        public static Manager Instance { get; private set; }

        public void Awake() {
            Tilemap = gameObject.GetComponentInChildren<Tilemap>();
        }

        public void Start() {
            GetExistingMapItems();

            GenerateAdjacentWorlds();
        }

        public void OnEnable() {
            if (!Instance)
            {
                Instance = this;
            }

            MapItemSelect.AddListener(OnMapItemSelected);
        }

        public void OnDisable() {
            MapItemSelect.RemoveAllListeners();
        }

        public void OnMapItemSelected(MapItem mapItem) {
            if (MapItemSelected)
            {
                MapItemSelected.Select.Invoke(false);
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

            MapItemSelected = mapItem;
            MapItemSelected.Select.Invoke(true);
        }

        public void GetExistingMapItems() {
            OccupiedCells.Clear();

            foreach (var mapItem in GetComponentsInChildren<MapItem>())
            {
                if (IsCellAvailable(mapItem.HexCords))
                {
                    OccupiedCells.Add(mapItem.HexCords, mapItem);
                }
            }
        }

        public bool IsCellAvailable(Vector3Int hexCords) {
            return !OccupiedCells.TryGetValue(hexCords, out _);
        }

        public void PlaceObject(Vector3Int hexCords, GameObject objectPrefab) {
            if (!IsCellAvailable(hexCords)) return;

            var objectToPlace = Instantiate
                (
                    objectPrefab,
                    Tilemap.CellToWorld(hexCords),
                    Quaternion.identity,
                    Tilemap.gameObject.transform
                );

            if (objectToPlace.TryGetComponent(out MapItem mapItem))
            {
                OccupiedCells.Add(hexCords, mapItem);
                mapItem.AssignCords(hexCords, this);
            }
            else
            {
                Destroy(objectToPlace);
            }
        }

        public List<Vector3Int> GetEmptyNeighbors() {
            List<Vector3Int> emptyNeighbors = new();

            foreach (var occupiedCell in OccupiedCells.Keys)
            {
                AdjacencyMatrix.ForEach(adjVec => emptyNeighbors.Add(occupiedCell + adjVec));
            }


            emptyNeighbors = emptyNeighbors.Distinct().Where(cell => !OccupiedCells.Keys.Contains(cell)).ToList();

            return emptyNeighbors;
        }

        public void GenerateAdjacentWorlds() {
            GetEmptyNeighbors().ForEach(cell => PlaceObject(cell, WorldPrefab));
        }

    }
}