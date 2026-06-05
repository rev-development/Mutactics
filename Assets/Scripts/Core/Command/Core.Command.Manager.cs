using System.Collections.Generic;
using UnityEngine;

namespace Core.Command
{
    public class Manager : MonoBehaviour
    {

        public readonly Stack<IReversibleCommand> History = new();

        public static Manager Instance { get; private set; }

        private void Awake() {
            if (Instance != null
                && Instance != this)
            {
                Destroy(gameObject);

                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ExecuteCommand(IReversibleCommand cmd) {
            cmd.Execute();
            History.Push(cmd);
        }

        public void UndoLastCommand() {
            if (History.Count == 0) return;

            History.Pop().Undo();
        }

    }
}