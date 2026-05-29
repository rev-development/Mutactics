using UnityEngine;

namespace CampaignMap
{
    [CreateAssetMenu(fileName = "World", menuName = "Mutactics/CampaignMap/World")]
    public class WorldData : ScriptableObject, IWorldData
    {

        [field: SerializeField] public string ItemName { get; set; }
        [field: SerializeField] public Vector2Int MapSize { get; set; } = new(40, 40);
        [field: SerializeField] public Vector3Int Cell { get; set; }
        [field: SerializeField] public bool IsPlayerControlled { get; set; }
        [field: SerializeField] [field: Range(0, 6)]
        public int Altitude { get; set; } = 1;

        public void AssignData(WorldDataStruct data) {
            ItemName = data.ItemName;
            MapSize = data.MapSize;
            Cell = data.Cell;
            IsPlayerControlled = data.IsPlayerControlled;
        }

    }
}