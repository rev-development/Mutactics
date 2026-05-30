using System.Linq;
using AYellowpaper.SerializedCollections;
using BattleMap.Hex;
using CampaignMap.World;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMap
{
    public static class MapGen
    {

        public static void InitMapSize(Vector2Int mapSize, SerializedDictionary<Vector2Int, HexData> mapData) {
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

        public static SerializedDictionary<Vector2Int, HexData> GenerateBaseMapData(
            WorldSO worldSO,
            TileBase defaultTile,
            GameObject hexSpacer
        ) {
            var mapData = new SerializedDictionary<Vector2Int, HexData>();
            InitMapSize(worldSO.MapSize, mapData);

            foreach (var cell in mapData.Values)
            {
                cell.Tile = defaultTile;
            }

            return mapData;
        }

        public static SerializedDictionary<Vector2Int, HexData> GenerateMapData(
            WorldSO worldSO,
            TileBase defaultTile,
            GameObject hexSpacer
        ) {
            var mapData = GenerateBaseMapData(worldSO, defaultTile, hexSpacer);

            foreach (var hexData in mapData)
            {
                ApplyAltitude(hexData.Value, mapData, worldSO);
            }

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
            var generatedAltitude = Mathf.CeilToInt(Mathf.Clamp(randomAltitude, 0, worldSO.Altitude));
            hexData.Cell = new Vector3Int(hexData.Cell.x, hexData.Cell.y, generatedAltitude);
        }

    }
}