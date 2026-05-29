using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    public abstract class GridItem : MonoBehaviour, IGridItem
    {

        #region Events

        [Header("Grid Item Events")]
        public UnityEvent<bool> Select = new();

        #endregion

        #region Listeners

        protected virtual void OnSelect(bool isSelected) {
            IsSelected = isSelected;
        }

        #endregion

        public virtual void AssignCords(Vector3Int hexCords, TileBase tile, GameObject hexSpacer) {
            Cell = hexCords;
            Tile = tile;
            HexSpacer = hexSpacer;
        }

        protected void CenterSelf(Tilemap tilemap) {
            if (tilemap.WorldToCell(transform.position) != Cell)
            {
                transform.position = tilemap.CellToWorld(Cell);
            }
        }

        #region Lifecycle

        public virtual void OnEnable() {
            Select.AddListener(OnSelect);
        }

        public virtual void OnDisable() {
            Select.RemoveAllListeners();
        }

        #endregion

        #region Runtime Values

        [Header("Grid Item Runtime Values")]
        public Vector3Int Cell { get; set; }
        public TileBase Tile { get; set; }
        public GameObject HexSpacer { get; set; }
        public bool IsSelected { get; protected set; } = false;

        #endregion

    }
}