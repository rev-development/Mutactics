using System;
using Mapster;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map.GridItem
{
    [Serializable]
    public abstract class GridItemSO<TData, TDataInterface> : ScriptableObject, IGridItemData
        where TData : GridItemData, IGridItemData, TDataInterface
        where TDataInterface : IGridItemData
    {

        [field: SerializeField] public GameObject CorrespondingGameObject;

        [field: SerializeField] public Vector3Int Cell { get; set; } = new();

        [field: SerializeField] public TileBase Tile { get; set; } = null;

        public Vector2Int GetKey() {
            return default;
        }

        public virtual void AssignData(TData data, GameObject correspondingGameObject) {
            data.Adapt(this);
            CorrespondingGameObject = correspondingGameObject;
        }

    }
}