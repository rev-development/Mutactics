using BattleMap.Hex;
using CampaignMap.World;
using Core.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMap
{
    [AddComponentMenu("Battle Map Manager")]
    public class ManagerBase : ManagerBase<ManagerBase, IHexData, HexSO, Hex.Hex, HexData>
    {

        public CampaignMap.Manager CampaignMapManager;
        public GameObject HexSpacer;
        public Tile SimpleColorHex;
        public WorldSO WorldData;

        public void ResetMap() {
            foreach (var hex in OccupiedCells.Values)
            {
            #if UNITY_EDITOR
                DestroyImmediate(hex.gameObject);
            #else
                Destroy(gridItem.HexSpacer);
            #endif
            }

            Tilemap.ClearAllTiles();

            OccupiedCells.Clear();
        }

        public void GenerateMap() {
            ResetMap();

            if (WorldData == null)
            {
                if (!CampaignMapManager)
                {
                    CampaignMapManager = CampaignMap.Manager.Instance as CampaignMap.Manager;
                }

                if (CampaignMapManager)
                {
                    if (CampaignMapManager.ActiveSelection)
                    {
                        WorldData = CampaignMapManager.ActiveSelection.DataSO;
                    }
                }
            }

            var mapData = MapGen.GenerateMapData(WorldData, SimpleColorHex, HexSpacer);

            foreach (var cellData in mapData)
            {
                PlaceObject(cellData.Value, HexSpacer);
            }
        }

    }
}