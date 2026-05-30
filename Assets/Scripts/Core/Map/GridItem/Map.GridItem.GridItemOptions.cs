using System;
using UnityEngine;

namespace Core.Map.GridItem
{
    [Serializable]
    public class GridItemOptions
    {

        [field: SerializeField] public bool PlaceObjectBelowGrid { get; set; } = false;

    }
}