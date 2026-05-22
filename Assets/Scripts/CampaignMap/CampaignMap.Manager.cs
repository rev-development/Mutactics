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

        public Tilemap Tilemap;

        public GameObject WorldPrefab;

        [SerializeField] public SerializedDictionary<Vector3Int, MapItem> OccupiedCells = new();

        public UnityEvent<MapItem> MapItemSelect = new();

        public MapItem MapItemSelected;

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

        public GameObject PlaceObject(Vector3Int hexCords, GameObject objectPrefab, string itemName = "MapItem") {
            if (!IsCellAvailable(hexCords)) return null;

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
                mapItem.AssignCords(hexCords, this, itemName);
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

        public void GenerateAdjacentWorlds() {
            var adjacentWorlds = GetEmptyNeighbors();
            var worldNames = Helpers.PlanetNameGen.GenerateNames(adjacentWorlds.Count);

            for (var index = 0; index < adjacentWorlds.Count; index++)
            {
                var cell = adjacentWorlds[index];
                var world = PlaceObject(cell, WorldPrefab, worldNames[index]);
            }
        }

    }
}