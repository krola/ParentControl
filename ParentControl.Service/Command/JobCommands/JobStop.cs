using System;

namespace ParentControl.Service.Command.JobCommands
{
    class JobStop : BaseCommand
    {
        public override string Command => "STOP";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " - Stop all jobs statuses");
            Console.WriteLine("\t" + Command + " jobId - Stop job with specific id");
        }

        protected override void Do(string[] args)
        {
            if (args.Length > 0)
            {
                Context.JobManager.Stop(args[0]);
            }
            else
            {
                Context.JobManager.Stop();
            }
        }
    }
}
