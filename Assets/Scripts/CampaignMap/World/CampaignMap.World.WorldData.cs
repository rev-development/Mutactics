using System;
using Core.Map.GridItem;
using UnityEngine;

namespace CampaignMap.World
{
    [Serializable]
    public class WorldData : GridItemData, IWorldData
    {

        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField] public Vector2Int MapSize { get; set; }

        [field: SerializeField] public bool IsPlayerControlled { get; set; }

        [field: SerializeField] public int AltitudeMax { get; set; }

    }
}