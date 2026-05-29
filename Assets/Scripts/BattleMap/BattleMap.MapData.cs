using System;
using AYellowpaper.SerializedCollections;
using Core.Map;
using UnityEngine;

namespace BattleMap
{
    [Serializable]
    public class MapData : SerializedDictionary<Vector2Int, GridItemData>
    {

    }
}