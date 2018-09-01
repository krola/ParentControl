using System;

namespace ParentControl.Service.Command.JobCommands
{
    class JobStart : BaseCommand
    {
        public override string Command => "START";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " - Start all jobs statuses");
            Console.WriteLine("\t" + Command + " jobId - Start job with specific id");
        }

        protected override void Do(string[] args)
        {
            if (args.Length > 0)
            {
                Context.JobManager.Start(args[0]);
            }
            else
            {
                Context.JobManager.Start();
            }
        }
    }
}
