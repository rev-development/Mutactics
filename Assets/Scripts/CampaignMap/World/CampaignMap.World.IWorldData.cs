using Core.Map.GridItem;
using UnityEngine;

namespace CampaignMap.World
{
    public interface IWorldData : IGridItemData
    {

        public string Name { get; set; }
        public Vector2Int MapSize { get; set; }
        public bool IsPlayerControlled { get; set; }
        public int AltitudeMax { get; set; }

    }
}