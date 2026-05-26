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

        public Tile SimpleColorHex;

        public GameObject WorldPrefab;

        [SerializeField] public SerializedDictionary<Vector3Int, MapItem> OccupiedCells = new();

        public UnityEvent<MapItem> MapItemSelect = new();

        public MapItem MapItemSelected;

        public static Manager Instance { get; private set; }

        public void Awake() {
            SingletonAwake();
        }

        public void Start() {
            GetExistingMapItems();

            GenerateAdjacentWorlds();
        }

        public void OnEnable() {
            MapItemSelect.AddListener(OnMapItemSelected);
        }

        public void OnDisable() {
            MapItemSelect.RemoveAllListeners();
        }

        private void SingletonAwake() {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
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

            foreach (var mapItem in Tilemap.GetComponentsInChildren<MapItem>())
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

            objectToPlace.transform.Translate(new Vector3(0, 0.125f, 0));

            if (objectToPlace.TryGetComponent(out MapItem mapItem))
            {
                OccupiedCells.Add(hexCords, mapItem);

                mapItem.AssignCords
                    (
                        hexCords,
                        this,
                        Tilemap.GetTile(hexCords),
                        itemName
                    );
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
                Tilemap.SetTile(cell, SimpleColorHex);

                Tilemap.SetColor
                    (
                        cell,
                        new Color
                            (
                                1f,
                                0f,
                                0f,
                                0.25f
                            )
                    );
            }
        }

    }
}