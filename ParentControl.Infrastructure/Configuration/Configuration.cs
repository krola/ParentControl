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

        public string ApiAddress => "http://50.3.70.54:5000/";

        public string Login
        {
            get => Store.Login;
            set { Store.Login = value; UpdateStore(); }
        }

        public string Password
        {
            get => Store.Password;
            set { Store.Password = value; UpdateStore(); }
        }

        public bool AutoLogin
        {
            get => Store.AutoLogin;
            set { Store.AutoLogin = value; UpdateStore(); }
        }
    }
}
