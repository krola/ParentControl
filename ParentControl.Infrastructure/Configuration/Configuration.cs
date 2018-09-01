using ParentControl.Infrastructure.Configuration.Model;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Storage;

namespace ParentControl.Infrastructure.Configuration
{
    public class Configuration : LocalStorageBase<ApiConfiguration>, IConfiguration
    {
        public Configuration() : base("AuthorizationConfig")
        {
            
        }

        public string ApiAddress {
            get => Store.ApiUrl;
            set { Store.ApiUrl = value; SaveStore(); }
        }

        public string Login
        {
            get => Store.Login;
            set { Store.Login = value; SaveStore(); }
        }

        public string Password
        {
            get => Store.Password;
            set { Store.Password = value; SaveStore(); }
        }
    }
}
