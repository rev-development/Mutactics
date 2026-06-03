using Core.Map.GridItem;

namespace Core.Map
{
    public interface IManagerConfig<TMono, TSO, TDto, TI>
        where TMono : GridItem<TSO, TDto, TI>
        where TSO : GridItemSO<TDto, TI>, TI
        where TDto : GridItemData, TI
        where TI : IGridItemData
    {

        TMono MonoBehaviourType { get; }

        TSO ScriptableObjectType { get; }

        TDto DataTransferObjectType { get; }

        TI DataInterfaceType { get; }

    }
}