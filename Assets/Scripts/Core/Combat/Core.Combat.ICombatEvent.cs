using Core.Mutation;

namespace Core
{
    public interface ICombatEvent : ICommand, IEvolPressure
    {

        public string TargetId { get; set; }

    }
}