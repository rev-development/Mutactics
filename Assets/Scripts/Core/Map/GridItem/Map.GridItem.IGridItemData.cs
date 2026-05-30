using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    public interface IGridItemData
    {

        public Vector3Int Cell { get; set; }
        public TileBase Tile { get; set; }

    }
}