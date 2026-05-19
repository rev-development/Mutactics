using Core.UnitProgression;

namespace Core
{
    public interface ICombatEvent : ICommand, IEvolPressure
    {

        public string TargetId { get; set; }

    }
}