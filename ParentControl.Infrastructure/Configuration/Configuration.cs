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

        public string ApiAddress => "http://parentcontrolapi.azurewebsites.net";
        public string Login { get { return Store.Login; } set { Store.Login = value; UpdateConfig(); } }
        public string Password { get { return Store.Password; } set { Store.Password = value; UpdateConfig(); } }
        public bool AutoLogin { get { return Store.AutoLogin; } set { Store.AutoLogin = value; UpdateConfig(); } }
    }
}
