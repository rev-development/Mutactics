using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    [Serializable]
    public abstract class GridItemSO<TDataInterface> : ScriptableObject, IGridItemData
        where TDataInterface : IGridItemData
    {

        [field: SerializeField] public GameObject CorrespondingGameObject { get; set; }
        [field: SerializeField] public Vector3Int Cell { get; set; } = new();
        [field: SerializeField] public TileBase Tile { get; set; }

        public Vector2Int GetKey() {
            return new Vector2Int(Cell.x, Cell.y);
        }

        public virtual void AssignData(TDataInterface worldData, GameObject correspondingGameObject) {
            Cell = worldData.Cell;
            Tile = worldData.Tile;
            CorrespondingGameObject = correspondingGameObject;
        }

    }
}