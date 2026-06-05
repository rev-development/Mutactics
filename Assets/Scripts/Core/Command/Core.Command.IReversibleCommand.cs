namespace Core.Command
{
    public interface IReversibleCommand : ICommand
    {

        void Undo();

    }
}