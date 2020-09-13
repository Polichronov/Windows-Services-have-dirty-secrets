using System.ServiceProcess;

namespace Demo1
{
    public partial class Demo1 : ServiceBase
    {
        public Demo1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        public void StartDemo(string[] args)
        {
            OnStart(args);
        }


        protected override void OnStop()
        {
        }
    }
}
