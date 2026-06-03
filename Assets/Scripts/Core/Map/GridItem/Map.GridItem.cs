using UnityEngine;

namespace Core.Map.GridItem
{
    public abstract class GridItem<TScriptableObject, TData, TDataInterface> : MonoBehaviour
        where TScriptableObject : GridItemSO<TData, TDataInterface>, TDataInterface
        where TData : GridItemData, IGridItemData, TDataInterface
        where TDataInterface : IGridItemData
    {

        public TScriptableObject DataSO;

        public void OnDestroy() {
            Destroy(DataSO);
            DataSO = null;
        }

        public virtual void Init(TData gridItemData, GridItemOptions options) {
            DataSO = ScriptableObject.CreateInstance<TScriptableObject>();

            DataSO.AssignData(gridItemData, gameObject);

            InitTransform(gridItemData.Cell, options);
            // TODO: Set Material
        }

        protected virtual void InitTransformScale(GridItemOptions options) {
            var adjustedScale = gameObject.transform.localScale;

            if (options.OnPlaceStretchObjectY)
            {
                adjustedScale.y = DataSO.Cell.z;
            }

            gameObject.transform.localScale = adjustedScale;
        }

        protected virtual void SetPositionOffsetY(GridItemOptions options) {
            var adjustedLocalPosition = gameObject.transform.localPosition;

            if (gameObject.TryGetComponent<MeshCollider>(out var meshCollider))
            {
                adjustedLocalPosition.y += meshCollider.bounds.size.y
                                           * gameObject.transform.localScale.y
                                           / 2
                                           * (options.PlaceObjectBelowGrid ? -1 : 1);
            }

            gameObject.transform.localPosition = adjustedLocalPosition;
        }

        protected virtual void InitTransform(Vector3Int destinationCell, GridItemOptions options) {
            InitTransformScale(options);
            SetPositionOffsetY(options);
        }

    }
}