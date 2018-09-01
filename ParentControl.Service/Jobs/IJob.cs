using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    interface IJob
    {
        string ID { get; }
        bool KeepAlive { get; }
        JobState GetState();
        void Start();
        void Stop();
    }
}
