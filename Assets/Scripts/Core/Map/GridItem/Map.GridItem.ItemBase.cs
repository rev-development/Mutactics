using Core.Map.Manager;
using UnityEngine;

namespace Core.Map.GridItem
{
    public abstract class ItemBase : MonoBehaviour
    {

        public abstract SOBase DataSOBase { get; }

        public abstract void Init(Dto dto, Options options);

        protected virtual void InitTransformScale(Options options) {
            var adjustedScale = gameObject.transform.localScale;

            if (options.OnPlaceStretchObjectY)
            {
                adjustedScale.y = DataSOBase.Cell.z;
            }

            gameObject.transform.localScale = adjustedScale;
        }

        protected virtual void SetPositionOffsetY(Options options) {
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

        protected virtual void InitTransform(Vector3Int destinationCell, Options options) {
            InitTransformScale(options);
            SetPositionOffsetY(options);
        }

        public abstract void InspectorDestroy();

    }
}