using ParentControl.Core.Configuration.Model;
using ParentControl.Core.Contracts;
using ParentControl.Core.Storage;

namespace ParentControl.Core.Configuration
{
    public class Configuration : LocalStorageBase<Authorization>, IConfiguration
    {
        public Configuration() : base("AuthorizationConfig")
        {
            
        }

        public string ApiAddress => "http://parentcontrolapi.azurewebsites.net";
        public string Login { get { return Store.Login; } set { Store.Login = value; UpdateConfigAsync(); } }
        public string Password { get { return Store.Password; } set { Store.Password = value; UpdateConfigAsync(); } }
        public bool AutoLogin { get { return Store.AutoLogin; } set { Store.AutoLogin = value; UpdateConfigAsync(); } }
    }
}
