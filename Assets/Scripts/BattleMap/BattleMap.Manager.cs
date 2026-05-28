using CampaignMap;
using UnityEngine;

namespace BattleMap
{
    [AddComponentMenu("Battle Map Manager")]
    public class Manager : Core.Map.Manager<Manager, Hex>
    {

        public WorldData WorldData;
        public CampaignMap.Manager CampaignMapManager;

        [ContextMenu("Init")]
        public void Init() {
            if (WorldData)
            {
                GenerateBattleMap(WorldData.MapSize);

                return;
            }

            if (!CampaignMapManager)
            {
                CampaignMapManager = CampaignMap.Manager.Instance as CampaignMap.Manager;
            }

            if (CampaignMapManager
                && CampaignMapManager.ActiveSelection
                && CampaignMapManager.ActiveSelection.TryGetComponent<WorldData>(out var worldData))
            {
                WorldData = worldData;
                GenerateBattleMap(WorldData.MapSize);
            }
        }

        public void GenerateBattleMap(Vector2Int mapSize) {
            for (var x = 0; x < mapSize.x; x++)
            {
                for (var y = 0; y < mapSize.y; y++)
                {
                    Tilemap.SetTile(new Vector3Int(x, y, 0), SimpleColorHex);
                }
            }
        }

    }
}