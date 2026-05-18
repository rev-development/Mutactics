using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.World
{
    public class World : MonoBehaviour
    {

        public Vector3Int HexCords;

        public Tilemap Tilemap;

        private Vector3Int _cachedHexCords;

        public void Start() {
            Tilemap = GetComponentInParent<Tilemap>();
            HexCords = Tilemap.WorldToCell(transform.position);
        }

        public void ChangeColor(Color color) {
            // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        }

    }
}