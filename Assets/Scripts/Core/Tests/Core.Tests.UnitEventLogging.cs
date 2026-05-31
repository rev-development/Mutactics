using Core.Combat;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace Core.Tests
{
    public class UnitEventLogging
    {

        public Combat.Manager CombatManager;
        public Unit.Unit SourceUnit;
        public Unit.Unit TargetUnit;

        [UnitySetUp]
        public void Setup() {
            CombatManager = new Combat.Manager();

            SourceUnit = new Unit.Unit(GUID.Generate().ToString());

            CombatManager.Units.Add(SourceUnit);

            TargetUnit = new Unit.Unit(GUID.Generate().ToString());

            CombatManager.Units.Add(TargetUnit);
        }

        [UnityTest]
        public void LogEnemyKill() {
            var gameEvent = new UnitKilledEnemy(TargetUnit, SourceUnit);
            CombatManager.AddEvent(gameEvent);

            Assert.That(TargetUnit.TagBonuses, Contains.Item(gameEvent));

            // Assert.That(TargetUnit)
        }

    }
}