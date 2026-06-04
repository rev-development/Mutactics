using Core.Map.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Map.GridItem
{
    public abstract class ItemBase : MonoBehaviour
    {

        public Options CachedOptions = null;

        public UnityEvent<Vector3Int> PositionChange = new();

        public abstract SOBase DataSOBase { get; }

        private void OnEnable() {
            PositionChange.AddListener(OnPositionChange);
        }

        private void OnDisable() {
            PositionChange.RemoveAllListeners();
        }

        public abstract void Init(Dto dto, Options options);

        protected virtual void InitTransformScale(Options options) {
            var adjustedScale = gameObject.transform.localScale;

            if (options.OnPlaceStretchObjectY)
            {
                adjustedScale.y = DataSOBase.Cell.z;
            }

            gameObject.transform.localScale = adjustedScale;
        }

        protected virtual void SetPositionOffsetY(Options options = null) {
            var adjustedLocalPosition = gameObject.transform.localPosition;

            if (gameObject.TryGetComponent<MeshCollider>(out var meshCollider))
            {
                adjustedLocalPosition.y += meshCollider.bounds.size.y
                                           * gameObject.transform.localScale.y
                                           / 2
                                           * (GetOptions(options).PlaceObjectBelowGrid ? -1 : 1);
            }

            gameObject.transform.localPosition = adjustedLocalPosition;
        }

        protected virtual void OnPositionChange(Vector3Int to) {
            DataSOBase.Cell = to;
            SetPositionOffsetY();
        }

        protected Options GetOptions(Options options = null) {
            if (options == null) return CachedOptions;

            CachedOptions = options;

            return options;
        }

        protected virtual void InitTransform(Options options) {
            InitTransformScale(options);
            SetPositionOffsetY(options);
        }

        public abstract void InspectorDestroy();

    }
}