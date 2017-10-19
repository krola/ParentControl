using ParentControl.Infrastructure.Contracts;

namespace ParentControl.Infrastructure.Configuration
{
    public class MobileConfiguration : IConfiguration
    {
        public string ApiAddress => "http://parentcontrolapi.azurewebsites.net";
        public string Login { get; set; }
        public string Password { get; set; }
        public bool AutoLogin { get; set; }
    }
}
