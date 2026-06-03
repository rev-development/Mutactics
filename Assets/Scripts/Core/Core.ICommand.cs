namespace Core
{
    public interface ICommand
    {

        public void Execute();

    }

    public interface IReversibleCommand : ICommand
    {

        void Undo();

    }
}