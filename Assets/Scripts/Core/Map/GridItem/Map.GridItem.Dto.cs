using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    /// <summary>
    ///     Used primarily for passing data to the GridItemSO when instantiating it.
    /// </summary>
    [Serializable]
    public class Dto : IDto
    {

        [field: SerializeField] public GameObject CorrespondingGameObject;

        [field: SerializeField] public Vector3Int Cell { get; set; }

        [field: SerializeField] public TileBase Tile { get; set; }

        public Vector2Int GetKey() {
            return default;
        }

    }
}