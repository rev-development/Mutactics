using Core;
using UnityEngine;

namespace BattleMap.Pawn.Command
{
    public class Move : IReversibleCommand
    {

        public Move(Pawn pawn, Hex.Hex hex, Vector3Int from, Vector3Int to) {
            Pawn = pawn;
            Hex = hex;
            From = from;
            To = to;
        }

        public Vector3Int From { get; set; }

        public Hex.Hex Hex { get; set; }

        public Pawn Pawn { get; set; }

        public Vector3Int To { get; set; }

        public void Execute() {
            BattleMap.Pawn.Manager.Instance.MoveObject(Pawn, From, To);
        }

        public void Undo() {
            BattleMap.Pawn.Manager.Instance.MoveObject(Pawn, To, From);
        }

    }
}