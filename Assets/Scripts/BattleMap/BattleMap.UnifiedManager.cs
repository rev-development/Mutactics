using UnityEngine;

namespace BattleMap
{
    [RequireComponent(typeof(Hex.Manager))] [RequireComponent(typeof(Pawn.Manager))]
    public class UnifiedManager : MonoBehaviour
    {

        // public LayerMask LayerMask
        // {
        //     get
        //     {
        //         if (PawnManager.ActiveSelection)
        //         {
        //             return 
        //         }
        //     }
        // };
        public Hex.Manager HexManager;
        public Pawn.Manager PawnManager;

        public void Awake() {
            HexManager = Hex.Manager.Instance as Hex.Manager;
            PawnManager = Pawn.Manager.Instance as Pawn.Manager;
        }

    }
}