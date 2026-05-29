using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    public interface IGridItem
    {

        public Vector3Int Cell { get; set; }
        public TileBase Tile { get; set; }
        public GameObject HexSpacer { get; set; }

    }
}