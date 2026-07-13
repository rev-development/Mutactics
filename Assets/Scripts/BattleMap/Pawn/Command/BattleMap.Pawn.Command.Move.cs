using Core.Command;
using UnityEngine;

namespace BattleMap.Pawn.Command
{
	public class Move : Move<BattleMap.Pawn.Manager, Pawn>
	{

		public Move(Manager manager, Pawn item, Vector3Int from, Vector3Int to) : base(
				manager,
				item,
				from,
				to
			) {
		}

	}
}