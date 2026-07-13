using System.Collections.Generic;
using BattleMap.Pawn.Command;
using Core.Command;
using Rev.Helpers;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BattleMap
{
	[RequireComponent(typeof(Hex.Manager))] [RequireComponent(typeof(Pawn.Manager))]
	public class UnifiedManager : SingletonMonoBehaviour<UnifiedManager>
	{

		public Hex.Manager HexManager;

		public Pawn.Manager PawnManager;

		public readonly Stack<IReversibleCommand> History = new();

		public void OnEnable() => HexManager.GridItemSelected.AddListener(_ => QueuePawnMove());

		public void Start() {
			HexManager = Hex.Manager.Instance;
			PawnManager = Pawn.Manager.Instance;
		}

		public static int GetSelectionLayerMask() {
			var layerMask = Pawn.Manager.Instance.ActiveSelection != null
				? LayerMask.GetMask("Hex")
				: LayerMask.GetMask("Pawn");

			return layerMask;
		}

		public void QueuePawnMove() {
			if (PawnManager.ActiveSelection
				&& HexManager.ActiveSelection)
			{
				var cmd = new Move(
						PawnManager,
						PawnManager.ActiveSelection,
						PawnManager.ActiveSelection.DataSO.Cell,
						HexManager.ActiveSelection.DataSO.Cell
					);

				Debug.Log(
						$"Move Cmd: {PawnManager.ActiveSelection.name} at {PawnManager.ActiveSelection.DataSO.Cell}"
						+ $" to {HexManager.ActiveSelection.name} at {HexManager.ActiveSelection.DataSO.Cell}"
					);

				// TODO: Need to add gating
				ExecuteCommand(cmd);
			}
		}

		public void ExecuteCommand(IReversibleCommand cmd) {
			cmd.Execute();
			History.Push(cmd);
		}

		public void UndoLastCommand() {
			if (History.Count == 0) return;

			History.Pop().Undo();
		}

		public static void Deselect(bool deselectAll = false) {
			if (deselectAll)
			{
				Hex.Manager.Instance.SelectGridItem(null);
				Pawn.Manager.Instance.SelectGridItem(null);
			}

			if (Hex.Manager.Instance.ActiveSelection)
			{
				Hex.Manager.Instance.SelectGridItem(null);
			}

			else if (Pawn.Manager.Instance.ActiveSelection)
			{
				Pawn.Manager.Instance.SelectGridItem(null);
			}
		}

	}
}