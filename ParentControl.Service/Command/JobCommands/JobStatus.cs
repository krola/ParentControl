using System;

namespace ParentControl.Service.Command.JobCommands
{
    class JobStatus : BaseCommand
    {
        public override string Command => "STATUS";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " - Show jobs statuses");
        }

        protected override void Do(string[] args)
        {
            Context.JobManager.Status();
        }
    }
}
