using System.Collections.Generic;
using UnityEngine;

namespace Core.Combat
{
    public sealed class Manager : ScriptableObject
    {

        public List<Unit.Unit> Units = new();

        public static Manager Instance { get; private set; }

        public Stack<ICombatEvent> History { get; } = new();

        public void OnEnable() {
            if (!Instance)
            {
                Instance = this;
            }
        }

        public void AddEvent(ICombatEvent combatEvent) {
            History.Push(combatEvent);
            combatEvent.Execute();
        }

    }
}