using Core.UnitProgression;

namespace Core.Command
{
    public interface ICommand : IEvolPressure
    {

        public void Execute();

    }
}