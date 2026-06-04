using System;
using Mapster;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    [Serializable]
    public abstract class SOBase : ScriptableObject, IDto
    {

        [field: SerializeField] public Vector3Int Cell { get; set; }

        [AdaptIgnore]
        [field: SerializeField] public TileBase Tile { get; set; }

    }
}