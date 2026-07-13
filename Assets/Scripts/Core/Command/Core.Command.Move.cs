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

		public Vector3Int From;

		public Vector3Int To;

		public Move(TManager manager, TItem item, Vector3Int from, Vector3Int to) {
			Manager = manager;
			Item = item;
			From = from;
			To = to;
		}

		public void Execute() => Manager.MoveObject(Item, From, To);

		public void Undo() => Manager.MoveObject(Item, To, From);

		public Dictionary<string, float> TagBonuses { get; }

	}
}