using CampaignMap.World;
using Core.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CampaignMap
{
    [AddComponentMenu("Campaign Map Manager")]
    public class Manager : ManagerBase<Manager, World.World, WorldSO, WorldData, IWorldData>
    {

        public Tile SimpleColorHex;

        public GameObject WorldPrefab;

        public Vector2Int DefaultMapSize = new(40, 40);

        public override void Start() {
            base.Start();
            GenerateAdjacentWorlds();
        }

        public void GenerateAdjacentWorlds() {
            var adjacentWorlds = GetEmptyNeighbors();
            var worldNames = Helpers.PlanetNameGen.GenerateNames(adjacentWorlds.Count);

            for (var index = 0; index < adjacentWorlds.Count; index++)
            {
                var worldDataStruct = new WorldData
                {
                    Name = worldNames[index],
                    Cell = adjacentWorlds[index]
                };

                // TODO: Randomize Map Size

                var world = PlaceObject(
                        new WorldData
                        {
                            Cell = adjacentWorlds[index],
                            Tile = SimpleColorHex,
                            Name = worldNames[index],
                            MapSize = DefaultMapSize,
                            IsPlayerControlled = false,
                            AltitudeMax = 0
                        },
                        WorldPrefab
                    );


                Tilemap.SetColor(
                        worldDataStruct.Cell,
                        new Color(
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