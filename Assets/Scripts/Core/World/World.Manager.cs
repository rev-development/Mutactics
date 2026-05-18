using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.World
{
    public class Manager : MonoBehaviour
    {

        public Tilemap Tilemap;

        public GameObject WorldPrefab;

        public SerializedDictionary<Vector3Int, World> Worlds = new();

        public static Manager Instance { get; private set; }

        public void Start() {
            AddWorld(Vector3Int.zero);
            AddWorld(Vector3Int.right);
            GenerateAdjacentWorlds();

            foreach (var world in Worlds.Values)
            {
                world.name = world.HexCords.ToString();
            }
        }

        public void OnEnable() {
            if (!Instance)
            {
                Instance = this;
            }
        }

        public World AddWorld(Vector3Int hexCords) {
            var world = Instantiate
                (
                    WorldPrefab,
                    Tilemap.CellToWorld(hexCords),
                    Quaternion.identity,
                    Tilemap.gameObject.transform
                );

            Worlds.TryAdd(hexCords, world.GetComponent<World>());

            return Worlds[hexCords];
        }

        public void GenerateAdjacentWorlds() {
            var hexAdjVec = new List<Vector3Int>
            {
                new(-1, 0, 0),
                new(1, 0, 0),
                new(0, 1, 0),
                new(-1, 1, 0),
                new(0, -1, 0),
                new(-1, -1, 0)
            };

            var possibleCandidates = new List<Vector3Int>();

            foreach (var hexCord in Worlds.Keys)
            {
                hexAdjVec.ForEach(adjVec => { possibleCandidates.Add(hexCord + adjVec); });
            }

            possibleCandidates = possibleCandidates.Distinct().ToList();

            possibleCandidates = possibleCandidates.Where(candidate => !Worlds.Keys.Contains(candidate)).ToList();

            possibleCandidates.ForEach
                (candidate =>
                    {
                        var world = AddWorld(candidate);
                        world.ChangeColor(Color.red);
                    }
                );
        }

    }
}