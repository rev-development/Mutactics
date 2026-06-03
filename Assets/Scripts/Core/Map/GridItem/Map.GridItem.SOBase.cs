using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    public abstract class SOBase : ScriptableObject, IDto
    {

        public Vector3Int Cell { get; set; }

        public TileBase Tile { get; set; }

        public Vector2Int GetKey() {
            return default;
        }

    }
}