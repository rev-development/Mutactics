using Core.Map;
using UnityEngine;

namespace CampaignMap
{
    [AddComponentMenu("Campaign Map Manager")]
    public class Manager : Manager<Manager, World>
    {

        public GameObject WorldPrefab;

        public void Start() {
            GetExistingGridItems();
            GenerateAdjacentWorlds();
        }

        public GameObject PlaceWorld(GameObject objectPrefab, WorldDataStruct worldDataStruct) {
            var placedObject = base.PlaceObject(worldDataStruct.Cell, objectPrefab);
            // placedObject.gameObject.transform.Translate(0, 0.125f, 0);

            if (placedObject.gameObject.TryGetComponent(out World world))
            {
                world.AssignData(worldDataStruct);
            }

            return placedObject;
        }

        public void GenerateAdjacentWorlds() {
            var adjacentWorlds = GetEmptyNeighbors();
            var worldNames = Helpers.PlanetNameGen.GenerateNames(adjacentWorlds.Count);

            for (var index = 0; index < adjacentWorlds.Count; index++)
            {
                var worldDataStruct = new WorldDataStruct
                {
                    ItemName = worldNames[index],
                    MapSize = new Vector2Int(20, 20),
                    Cell = adjacentWorlds[index]
                };

                // TODO: Randomize Map Size

                var world = PlaceWorld(WorldPrefab, worldDataStruct);
                Tilemap.SetTile(worldDataStruct.Cell, SimpleColorHex);

                Tilemap.SetColor
                    (
                        worldDataStruct.Cell,
                        new Color
                            (
                                1f,
                                0f,
                                0f,
                                0.25f
                            )
                    );
            }
        }

    }
}