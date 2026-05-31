using System.Collections.Generic;
using Core.UnitProgression;

namespace Core.Combat
{
    public class UnitKilledEnemy : ICombatEvent, IEvolPressure
    {

        public UnitKilledEnemy(Unit.Unit targetUnit, Unit.Unit sourceUnit) {
            TargetId = targetUnit.Id;
        }

        public void Execute() {
            Core.Combat.Manager.Instance.Units.Find(unit => unit.Id == TargetId).EvolPressures.Add(this);
        }

        public string TargetId { get; set; }
        public Dictionary<string, float> TagBonuses { get; }

    }
}