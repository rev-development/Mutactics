using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    public interface IDto
    {

        public Vector3Int Cell { get; set; }

        public TileBase Tile { get; set; }

        public Vector2Int GetKey() {
            return new Vector2Int(Cell.x, Cell.y);
        }

    }
}