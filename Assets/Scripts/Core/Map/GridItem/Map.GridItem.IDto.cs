using Mapster;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    public interface IDto
    {

        public Vector3Int Cell { get; set; }

        [AdaptIgnore]
        public TileBase Tile { get; set; }

        public Vector2Int Key { get; }

    }
}