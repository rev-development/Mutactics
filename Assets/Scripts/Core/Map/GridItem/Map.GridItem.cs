using UnityEngine;
using UnityEngine.Events;

namespace Core.Map.GridItem
{
    [RequireComponent(typeof(Outline))]
    public abstract class GridItem<TDataInterface, TScriptableObject> : MonoBehaviour
        where TScriptableObject : GridItemSO<TDataInterface> where TDataInterface : IGridItemData
    {

        [field: SerializeField] private Vector3Int _inspectorHex;
        public TScriptableObject DataSO;
        public UnityEvent<bool> Select = new();
        public Outline Outline;
        public bool IsSelected { get; protected set; } = false;
        public GameObject CorrespondingGameObject { get; set; }

        public virtual void OnEnable() {
            if (!Outline)
            {
                Outline = gameObject.GetComponent<Outline>();
            }

            Outline.enabled = IsSelected;

            Select.AddListener(OnSelect);
        }

        public virtual void OnDisable() {
            Select.RemoveAllListeners();
        }

        public void OnDestroy() {
            Destroy(DataSO);
            DataSO = null;
        }

        protected virtual void OnSelect(bool isSelected) {
            IsSelected = isSelected;
            Outline.enabled = IsSelected;
        }

        public virtual void Init(TDataInterface gridItemData, GridItemOptions options) {
            DataSO = ScriptableObject.CreateInstance<TScriptableObject>();

            DataSO.AssignData(gridItemData, gameObject);

            _inspectorHex = DataSO.Cell;

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
            var adjustedPosition = gameObject.transform.position;

            if (options.OffsetProBuilderMesh
                && gameObject.TryGetComponent<MeshFilter>(out var proBuilderMesh))
            {
                adjustedPosition.y += proBuilderMesh.sharedMesh.bounds.size.y
                                      * gameObject.transform.localScale.y
                                      / 2
                                      * (options.PlaceObjectBelowGrid ? -1 : 1);
            }

            gameObject.transform.position = adjustedPosition;
        }

        protected virtual void InitTransform(GridItemOptions options) {
            InitTransformScale(options);
            InitTransformPosition(options);
        }

    }
}