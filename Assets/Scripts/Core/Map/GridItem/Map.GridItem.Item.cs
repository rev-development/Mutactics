using Core.Map.Manager;
using UnityEngine;

namespace Core.Map.GridItem
{
    public abstract class Item<TSO, TDto, TIDto> : ItemBase
        where TSO : SO<TDto, TIDto>, TIDto
        where TDto : Dto, IDto, TIDto
        where TIDto : IDto
    {

        public TSO DataSO;

        public override SOBase DataSOBase => DataSO;

        private void OnDestroy() {
            Destroy(DataSOBase);
            DataSO = null;
        }

        public override void InspectorDestroy() {
            DestroyImmediate(DataSOBase);
            DataSO = null;
            DestroyImmediate(gameObject);
        }

        public override void Init(Dto dto, Options options) {
            Init((TDto)dto, options);
        }

        public virtual void Init(TDto gridItemData, Options options) {
            DataSO = ScriptableObject.CreateInstance<TSO>();

            DataSO.AssignData(gridItemData, gameObject);

            InitTransform(gridItemData.Cell, options);
            // TODO: Set Material
        }

    }
}