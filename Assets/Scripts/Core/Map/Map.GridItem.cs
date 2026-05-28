using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    public abstract class GridItem : MonoBehaviour
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

        public virtual void AssignCords(Vector3Int hexCords, TileBase tile) {
            Cell = hexCords;
            Tile = tile;
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
        public Vector3Int Cell;
        public TileBase Tile;
        public bool IsSelected { get; protected set; } = false;

        #endregion

    }
}