using UnityEngine;
using UnityEngine.Events;

namespace Core.Map.GridItem
{
    [RequireComponent(typeof(Outline))]
    public abstract class GridItem<TDataInterface, TScriptableObject> : MonoBehaviour
        where TScriptableObject : GridItemSO<TDataInterface> where TDataInterface : IGridItemData
    {

        [Helpers.InlineSOAttribute] public TScriptableObject DataSO;
        public IGridItemData CachedData;
        public UnityEvent<bool> Select = new();
        private Outline _outline;
        public bool IsSelected { get; protected set; } = false;

        public virtual void Awake() {
            _outline = GetComponent<Outline>();
        }

        public virtual void OnEnable() {
            if (!_outline)
            {
                _outline = gameObject.GetComponent<Outline>();
            }

            _outline.enabled = IsSelected;

            Select.AddListener(OnSelect);
        }

        public virtual void OnDisable() {
            Select.RemoveAllListeners();
        }

        public void OnDestroy() {
            Destroy(DataSO);
            DataSO = null;
        }

        public void OnApplicationQuit() {
        }

        protected virtual void OnSelect(bool isSelected) {
            IsSelected = isSelected;
            _outline.enabled = IsSelected;
        }

        public virtual void Init(TDataInterface gridItemData, GridItemOptions options) {
            DataSO = ScriptableObject.CreateInstance<TScriptableObject>();

            DataSO.AssignData(gridItemData, gameObject);

            InitTransform(options);
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

        protected virtual void InitTransformPosition(GridItemOptions options) {
            var adjustedLocalPosition = gameObject.transform.localPosition;

            if (gameObject.TryGetComponent<MeshCollider>(out var meshCollider))
            {
                adjustedLocalPosition.y += meshCollider.bounds.size.y
                                           * gameObject.transform.localScale.y
                                           / 2
                                           * (options.PlaceObjectBelowGrid ? -1 : 1);
            }
            //
            // if (options.OffsetProBuilderMesh
            //     && gameObject.TryGetComponent<MeshFilter>(out var proBuilderMesh))
            // {
            //     adjustedPosition.y += proBuilderMesh.sharedMesh.bounds.size.y
            //                           * gameObject.transform.localScale.y
            //                           / 2
            //                           * (options.PlaceObjectBelowGrid ? -1 : 1);
            // }

            gameObject.transform.localPosition = adjustedLocalPosition;
        }

        protected virtual void InitTransform(GridItemOptions options) {
            InitTransformScale(options);
            InitTransformPosition(options);
        }

    }
}