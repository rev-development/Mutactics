using System.Collections.Generic;

namespace Core.Combat
{
    public class UnitKilledEnemy : ICombatEvent
    {

        public UnitKilledEnemy(Unit.Unit targetUnit, Unit.Unit sourceUnit) {
            TargetId = targetUnit.Id;
        }

        public void Execute() {
            Core.Combat.Manager.Instance.Units.Find(unit => unit.Id == TargetId).EvolPressures.Add(this);
        }

        public Dictionary<string, float> TagBonuses { get; }

        public string TargetId { get; set; }

    }
}