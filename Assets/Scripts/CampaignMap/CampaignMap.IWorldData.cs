using UnityEngine;

namespace CampaignMap
{
    public interface IWorldData
    {

        public string ItemName { get; set; }
        public Vector2Int MapSize { get; set; }
        public Vector3Int Cell { get; set; }
        public bool IsPlayerControlled { get; set; }

    }
}