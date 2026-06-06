using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Command
{
    [Serializable]
    public class Move<TManager, TItem> : IReversibleCommand
        where TManager : Map.Manager.ManagerBase<TManager, TItem>
        where TItem : Map.GridItem.ItemBase
    {

        public TItem Item;

        public TManager Manager;

        public Vector3Int? From = null;

        public Vector3Int? To = null;

        public void Execute() {
            if (Manager
                && Item
                && From != null
                && To != null)
            {
                Manager.MoveObject(Item, (Vector3Int)From, (Vector3Int)To);
            }
        }

        public void Undo() {
            if (Manager
                && Item
                && From != null
                && To != null)
            {
                Manager.MoveObject(Item, (Vector3Int)To, (Vector3Int)From);
            }
        }

        public Dictionary<string, float> TagBonuses { get; }

    }
}