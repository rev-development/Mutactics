using CampaignMap.World;
using Core.Map;
using Core.Map.GridItem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMap.Hex
{
    [AddComponentMenu("Battle Map Hex Manager")]
    public class Manager : ManagerBase<Manager, IHexData, HexSO, Hex, HexData>
    {

        public GridItemOptions GridItemOptions = new()
        {
            ClearTilemapOnReset = true,
            OnPlaceStretchObjectY = true,
            PlaceObjectBelowGrid = true,
            OffsetProBuilderMesh = true
        };
        public CampaignMap.Manager CampaignMapManager;
        public GameObject HexSpacer;
        public Tile SimpleColorHex;
        public WorldSO WorldData;

        public void GenerateMap() {
            ResetMap();

            if (WorldData == null)
            {
                if (!CampaignMapManager)
                {
                    CampaignMapManager ??= CampaignMap.Manager.Instance as CampaignMap.Manager;
                }

                if (CampaignMapManager)
                {
                    if (CampaignMapManager.ActiveSelection)
                    {
                        WorldData ??= CampaignMapManager.ActiveSelection.DataSO;
                    }
                }
            }

            var mapData = MapGen.GenerateMapData(WorldData, SimpleColorHex);

            foreach (var cellData in mapData)
            {
                var hex = PlaceObject(cellData.Value, HexSpacer);
            }
        }

    }
}