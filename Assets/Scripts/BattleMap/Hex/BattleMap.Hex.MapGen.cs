using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using CampaignMap.World;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMap.Hex
{
    public static class MapGen
    {

        private static void InitMapSize(Vector2Int mapSize, SerializedDictionary<Vector2Int, HexData> mapData) {
            for (var x = 0; x < mapSize.x; x++)
            {
                for (var y = 0; y < mapSize.y; y++)
                {
                    mapData.Add(
                            new Vector2Int(x, y),
                            new HexData
                            {
                                Cell = new Vector3Int(x, y, 0)
                            }
                        );
                }
            }
        }

        private static SerializedDictionary<Vector2Int, HexData> GenerateBaseMapData(
            WorldSO worldSO,
            TileBase defaultTile
        ) {
            var mapData = new SerializedDictionary<Vector2Int, HexData>();
            InitMapSize(worldSO.MapSize, mapData);

            foreach (var cell in mapData.Values)
            {
                cell.Tile = defaultTile;
            }

            return mapData;
        }

        public static SerializedDictionary<Vector2Int, HexData> GenerateMapData(WorldSO worldSO, TileBase defaultTile) {
            var mapData = GenerateBaseMapData(worldSO, defaultTile);


            GenerateZValues(mapData, worldSO);


            return mapData;
        }

        public static void ApplyAltitude(
            HexData hexData,
            SerializedDictionary<Vector2Int, HexData> mapData,
            WorldSO worldSO
        ) {
            var adjacentCellCords = Helpers.HexMap.GetAdjacentCells(hexData.Cell)
                                           .Select(cell => new Vector2Int(cell.x, cell.y));

            var adjacentCells = mapData.Where(cell => adjacentCellCords.Contains(cell.Key)).ToList();
            var altitudeRangeMin = Mathf.Min(adjacentCells.Select(cell => cell.Value.Cell.z).ToArray()) - 1f;
            var altitudeRangeMax = Mathf.Max(adjacentCells.Select(cell => cell.Value.Cell.z).ToArray()) + 1f;
            var randomAltitude = Random.Range(altitudeRangeMin, altitudeRangeMax);

            // Min is 1 because of object scaling
            var generatedAltitude = Mathf.CeilToInt(Mathf.Clamp(randomAltitude, 1, worldSO.AltitudeMax));

            hexData.Cell = new Vector3Int(hexData.Cell.x, hexData.Cell.y, generatedAltitude);
        }

        public static SerializedDictionary<Vector2Int, HexData> GetEdgeHexes(
            SerializedDictionary<Vector2Int, HexData> mapData,
            Vector2Int mapSize
        ) {
            var edgeHexKeys = Helpers.HexMap.GetEdgeCells(mapData.Values.Select(cell => cell.Key).ToList(), mapSize);

            var edgeHexes
                = new SerializedDictionary<Vector2Int, HexData>(mapData.Where(cell => edgeHexKeys.Contains(cell.Key)));

            return edgeHexes;
        }

        public static SerializedDictionary<Vector2Int, HexData> GetInteriorHexes(
            SerializedDictionary<Vector2Int, HexData> mapData,
            Vector2Int mapSize
        ) {
            var edgeHexKeys = Helpers.HexMap.GetEdgeCells(mapData.Values.Select(cell => cell.Key).ToList(), mapSize);

            var interiorHexes
                = new SerializedDictionary<Vector2Int, HexData>(mapData.Where(cell => !edgeHexKeys.Contains(cell.Key)));

            return interiorHexes;
        }

        public static void GenerateZValues(SerializedDictionary<Vector2Int, HexData> mapData, WorldSO worldSO) {
            // Edges are any hex with a 0 in the value
            var edgeHexes = GetEdgeHexes(mapData, worldSO.MapSize);
            var interiorHexes = GetInteriorHexes(mapData, worldSO.MapSize);

            // TODO: Hex Ordering means this method is better button not correct
            // For fixing seam
            var processingOrder = new List<KeyValuePair<Vector2Int, HexData>>();

            foreach (var edgeHex in edgeHexes)
            {
                ApplyAltitude(edgeHex.Value, edgeHexes, worldSO);
                processingOrder.Add(edgeHex);
            }

            processingOrder.Reverse();

            for (var i = 0; i < processingOrder.Count - 1; i++)
            {
                var current = processingOrder[i];
                var next = processingOrder[i + 1];

                var newCords = next.Value.Cell;

                if (next.Value.Cell.z - current.Value.Cell.z > 1)
                {
                    newCords.z -= next.Value.Cell.z - current.Value.Cell.z - 1;
                }
                else if (next.Value.Cell.z - current.Value.Cell.z < -1)
                {
                    newCords.z -= next.Value.Cell.z - current.Value.Cell.z + 1;
                }

                next.Value.Cell = newCords;
            }

            foreach (var interiorHex in interiorHexes)
            {
                ApplyAltitude(interiorHex.Value, mapData, worldSO);
            }
        }

    }
}