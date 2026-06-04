using System;
using Core.Map.GridItem;
using UnityEngine;

namespace CampaignMap.World
{
    [CreateAssetMenu(fileName = "World", menuName = "Mutactics/CampaignMap/World")]
    [Serializable]
    public class WorldSO : SO<WorldData, IWorldData>, IWorldData
    {

        [field: SerializeField] public string Name { get; set; } = "";

        [field: SerializeField] public Vector2Int MapSize { get; set; } = new();

        [field: SerializeField] public bool IsPlayerControlled { get; set; } = false;

        [field: SerializeField] [field: Range(0, 6)] public int AltitudeMax { get; set; } = 1;

    }
}