using System.ServiceProcess;

namespace ParentControl.Service
{
    public partial class ParentControl : ServiceBase
    {
        public ParentControl()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
           
        }
    }
}
