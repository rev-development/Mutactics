using UnityEngine;

namespace BattleMap
{
    [RequireComponent(typeof(Hex.Manager))] [RequireComponent(typeof(Pawn.Manager))]
    public class UnifiedManager : MonoBehaviour
    {

        public Hex.Manager HexManager;

        public Pawn.Manager PawnManager;

        public void Awake() {
            HexManager = Hex.Manager.Instance;
            PawnManager = Pawn.Manager.Instance;
        }

    }
}