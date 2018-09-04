using ParentControl.DTO;
using ParentControl.Infrastructure.Service;
using ParentControl.Service.Constants;
using ParentControl.Service.Jobs;
using ParentControl.Service.Manager;
using System;
using System.Collections.Generic;

namespace ParentControl.Service
{
    class App
    {
        private static App instance = null;
        private static readonly object padlock = new object();

        App()
        {
        }

        public Mode Mode { get; set; }
        public bool Initialized { get; set; }
        public ParentControlService ParentControlService;
        public JobManager JobManager;
        public WebsocketHandler WebsocketHandler;
        public Schedule Schedule;
        public Device Device;
        public List<Session> TodaySessions;
        public Session ActiveSession;
        public TimeSpan TimeLeft;

        public static App Context
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new App();
                    }
                    return instance;
                }
            }
        }
    }
}
