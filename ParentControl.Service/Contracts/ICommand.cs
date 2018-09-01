namespace ParentControl.Service.Contracts
{
    interface ICommand
    {
        string Command { get; }
        void Execute(string command);
        void PrintInfo();
    }
}
