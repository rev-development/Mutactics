using System;
using Core.Map.GridItem;
using UnityEngine;

namespace CampaignMap.World
{
    [CreateAssetMenu(fileName = "World", menuName = "Mutactics/World")]
    [Serializable]
    public class WorldSO : GridItemSO<IWorldData>, IWorldData
    {

        [field: SerializeField] public string Name { get; set; } = "";
        [field: SerializeField] public Vector2Int MapSize { get; set; } = new();
        [field: SerializeField] public bool IsPlayerControlled { get; set; } = false;
        [field: SerializeField] [field: Range(1, 6)] public int Altitude { get; set; } = 1;

        public override void AssignData(IWorldData worldData, GameObject correspondingGameObject) {
            base.AssignData(worldData, correspondingGameObject);
            Name = worldData.Name;
            MapSize = worldData.MapSize;
            Cell = worldData.Cell;
            IsPlayerControlled = worldData.IsPlayerControlled;
            Altitude = worldData.Altitude;
        }

    }
}