using System;
using UnityEngine;

namespace Core.Command
{
    public class SelectionState
    {

        public LayerMask TargetMask;

        public Type TargetType;

        public Component OnMouseRaycasted(Ray ray) {
            if (Physics.Raycast(
                        ray,
                        out var hit,
                        Mathf.Infinity,
                        TargetMask
                    ))
            {
                if (hit.collider.gameObject.TryGetComponent(TargetType, out var component))
                {
                    return component;
                }
            }

            return null;
        }

    }
}