using ParentControl.Infrastructure.Configuration.Model;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Storage;

namespace ParentControl.Infrastructure.Configuration
{
    public class Configuration : LocalStorageBase<Authorization>, IConfiguration
    {
        public Configuration() : base("AuthorizationConfig")
        {
            
        }

        public string ApiAddress => "http://192.168.1.200:5000/";
        //public string Login { get { return Store.Login; } set { Store.Login = value; UpdateStore(); } }
        //public string Password { get { return Store.Password; } set { Store.Password = value; UpdateStore(); } }

        public string Login { get { return "test"; } set { Store.Login = value; UpdateStore(); } }
        public string Password { get { return "test"; } set { Store.Password = value; UpdateStore(); } }
        public bool AutoLogin { get { return Store.AutoLogin; } set { Store.AutoLogin = value; UpdateStore(); } }
    }
}
