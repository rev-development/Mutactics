using System;
using UnityEngine;

namespace Core.Map.GridItem
{
    [Serializable]
    public class GridItemOptions
    {

        [field: SerializeField] public bool PlaceObjectBelowGrid { get; set; } = false;
        [field: SerializeField] public bool ClearTilemapOnReset { get; set; } = false;
        [field: SerializeField] public bool OnPlaceStretchObjectY { get; set; } = false;
        [field: SerializeField] public bool DeselectOnDoubleClick { get; set; } = false;

    }
}