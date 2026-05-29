using System.Linq;
using CampaignMap;
using Core.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMap
{
    public static class MapGen
    {

        public static void InitMapSize(Vector2Int mapSize, MapData mapData) {
            for (var x = 0; x < mapSize.x; x++)
            {
                for (var y = 0; y < mapSize.y; y++)
                {
                    mapData.Add(new Vector2Int(x, y), new GridItemData(new Vector3Int(x, y, 0)));
                }
            }
        }

        public static MapData GenerateBaseMapData(IWorldData worldData, TileBase defaultTile, GameObject hexSpacer) {
            var mapData = new MapData();
            InitMapSize(worldData.MapSize, mapData);

            foreach (var cell in mapData.Values)
            {
                cell.Tile = defaultTile;
            }

            return mapData;
        }

        public static MapData GenerateMapData(IWorldData worldData, TileBase defaultTile, GameObject hexSpacer) {
            var mapData = GenerateBaseMapData(worldData, defaultTile, hexSpacer);

            foreach (var cellData in mapData)
            {
                var adjacentCellCords = Helpers.HexMap.GetAdjacentCells(cellData.Value.Cell)
                                               .Select(cell => new Vector2Int(cell.x, cell.y));

                var adjacentCells = mapData.Where(cell => adjacentCellCords.Contains(cell.Key)).ToList();

                var altitudeRangeMin = Mathf.Min(adjacentCells.Select(cell => cell.Value.Cell.z).ToArray()) - 1f;
                var altitudeRangeMax = Mathf.Max(adjacentCells.Select(cell => cell.Value.Cell.z).ToArray()) + 1f;

                var randomAltitude = Random.Range(altitudeRangeMin, altitudeRangeMax);
                var generatedAltitude = Mathf.CeilToInt(Mathf.Clamp(randomAltitude, 0, worldData.Altitude));

                cellData.Value.Cell = new Vector3Int(cellData.Value.Cell.x, cellData.Value.Cell.y, generatedAltitude);
            }

            return mapData;
        }

    }
}