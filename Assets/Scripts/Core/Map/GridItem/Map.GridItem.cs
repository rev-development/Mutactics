using UnityEngine;
using UnityEngine.Events;

namespace Core.Map.GridItem
{
    public abstract class GridItem<TDataInterface, TScriptableObject> : MonoBehaviour
        where TScriptableObject : GridItemSO<TDataInterface> where TDataInterface : IGridItemData
    {

        public TScriptableObject DataSO;
        public UnityEvent<bool> Select = new();
        public bool IsSelected { get; protected set; } = false;
        public GameObject CorrespondingGameObject { get; set; }

        public virtual void OnEnable() {
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
        }

        public virtual void Init(TDataInterface gridItemData, GridItemOptions options) {
            DataSO = ScriptableObject.CreateInstance<TScriptableObject>();

            DataSO.AssignData(gridItemData, gameObject);


            InitTransform(options);
            // TODO: Set Material
        }

        protected virtual void InitTransform(GridItemOptions options) {
            var adjustedScale = gameObject.transform.localScale;
            adjustedScale.y = DataSO.Cell.z;
            gameObject.transform.localScale = adjustedScale;

            if (gameObject.TryGetComponent<MeshFilter>(out var proBuilderMesh))
            {
                var adjustedPosition = gameObject.transform.position;

                adjustedPosition.y += proBuilderMesh.sharedMesh.bounds.size.y
                                      * gameObject.transform.localScale.y
                                      / 2
                                      * (options.PlaceObjectBelowGrid ? -1 : 1);

                gameObject.transform.position = adjustedPosition;
            }
        }

    }
}