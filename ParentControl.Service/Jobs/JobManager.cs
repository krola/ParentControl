using ParentControl.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ParentControl.Service.Jobs
{
    class JobManager
    {
        private List<IJob> _jobs;
        private List<IJob> _keepAliveJobs;
        private bool _keepAliveFlag;

        public bool KeepAliveEnabled { get {
                return _keepAliveFlag;
            } set {
                if(value && !_keepAliveFlag)
                {
                    _keepAliveFlag = value;
                    StartKeepAlive();
                }
                else
                {
                    _keepAliveFlag = value;
                }
            } }

        public JobManager()
        {
            _jobs = new List<IJob>();
            _keepAliveJobs = new List<IJob>();

            foreach (Type type in Assembly.GetAssembly(typeof(IJob)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.GetInterfaces().Any(i => i == typeof(IJob))))
            {
                _jobs.Add((IJob)Activator.CreateInstance(type));
            }

            KeepAliveEnabled = true;
        }

        public void Start()
        {
            _jobs.ForEach(j => Start(j, false));
        }

        public void Start(string jobId)
        {
            var job = GetJob(jobId);
            Start(job);
        }

        public void Stop()
        {
            _jobs.ForEach(j => Stop(j));
        }

        public void Stop(string jobId)
        {
            var job = GetJob(jobId);
            Stop(job);
        }

        public void Status()
        {
            Console.WriteLine("JOB STATUS");
            Console.Write("KeepAlive protection: ");
            if (_keepAliveFlag)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enabled");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Disabled");
            }
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var job in _jobs)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{job.ID.PadRight(20)}\t\tKeepAlive:{job.KeepAlive}\t\tStatus: ");
                switch (job.GetState())
                {
                    case Consts.JobState.Disabled:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Disabled");
                        break;
                    case Consts.JobState.Finished:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Finished");
                        break;
                    case Consts.JobState.Running:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Running");
                    break;
                    case Consts.JobState.Stopped:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stopped");
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private IJob GetJob(string jobId)
        {
            var job = _jobs.FirstOrDefault(j => j.ID.Equals(jobId, StringComparison.InvariantCultureIgnoreCase));

            if (job == null)
            {
                throw new JobNotExist(jobId);
            }

            return job;
        }

        private void Stop(IJob job)
        {
            if (job.GetState() == Consts.JobState.Stopped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Job is already stoppped.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (job.GetState() == Consts.JobState.Disabled)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Job is disabled.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            job.Stop();

            if (job.KeepAlive && _keepAliveJobs.Contains(job))
            {
                //keep alive me be accessing the same time
                _keepAliveJobs.Remove(job);
            }
        }

        private void Start(IJob job, bool showMessages = true)
        {
            if(job.GetState() == Consts.JobState.Running)
            {
                if (showMessages)
                {
                    PrintMessage("Job is already running.", ConsoleColor.Yellow);
                }
                return;
            }

            if (job.GetState() == Consts.JobState.Disabled)
            {
                if (showMessages)
                {
                    PrintMessage("Job is disabled.", ConsoleColor.Yellow);
                }
                return;
            }

            job.Start();

            if (job.KeepAlive && !_keepAliveJobs.Contains(job))
            {
                _keepAliveJobs.Add(job);
            }
        }

        private void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void StartKeepAlive()
        {
            Task.Factory.StartNew(() =>
            {
                while (_keepAliveFlag)
                {
                    _keepAliveJobs
                    .Where(j => j.GetState().Equals(Consts.JobState.Stopped))
                    .ToList()
                    .ForEach(j => {
                        Console.WriteLine($"KeepAlive: Starting job {j.ID}");
                        j.Start(); });

                    Thread.Sleep(5000);
                }                
            });
        }
    }
}
