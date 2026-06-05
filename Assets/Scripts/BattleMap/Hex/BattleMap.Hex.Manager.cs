using CampaignMap.World;
using Core.Map.Manager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMap.Hex
{
    [AddComponentMenu("BattleMap/Hex/HexManager")]
    public class Manager : ManagerBase<Manager, Hex>
    {

        public CampaignMap.Manager CampaignMapManager;

        public GameObject HexSpacer;

        public Tile SimpleColorHex;

        public WorldSO WorldData;

        public override Options Options { get; } = new()
        {
            ClearTilemapOnReset = true,
            OnPlaceStretchObjectY = true,
            PlaceObjectBelowGrid = true
        };

        public void GenerateMap() {
            ResetMap();

            if (WorldData == null)
            {
                if (!CampaignMapManager)
                {
                    CampaignMapManager ??= CampaignMap.Manager.Instance;
                }

                if (CampaignMapManager && CampaignMapManager.ActiveSelection)
                {
                    WorldData ??= CampaignMapManager.ActiveSelection.DataSO;
                }
            }


            var mapData = MapGen.GenerateMapData(WorldData, SimpleColorHex);

            foreach (var cellData in mapData)
            {
                var world = PlaceObject(cellData.Value, HexSpacer);
            }
        }

    }
}