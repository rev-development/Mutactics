using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    [Serializable]
    public class GridItemData : IGridItem
    {

        public GridItemData(Vector3Int cell) {
            Cell = cell;
        }

        [field: SerializeField] public Vector3Int Cell { get; set; }
        [field: SerializeField] public TileBase Tile { get; set; }
        [field: SerializeField] public GameObject HexSpacer { get; set; }

    }
}