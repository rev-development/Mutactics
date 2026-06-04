using System;
using Mapster;
using UnityEngine;

namespace Core.Map.GridItem
{
    [Serializable]
    public abstract class SO<TDto, TIDto> : SOBase
        where TDto : Dto, IDto, TIDto
        where TIDto : IDto
    {

        [field: SerializeField] public GameObject CorrespondingGameObject;

        public virtual void AssignData(TDto data, GameObject correspondingGameObject) {
            data.Adapt(this);
            Tile = data.Tile;
            CorrespondingGameObject = correspondingGameObject;
        }

    }
}