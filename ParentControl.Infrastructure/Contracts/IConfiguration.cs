namespace ParentControl.Infrastructure.Contracts
{
    public interface IConfiguration
    {
        string ApiAddress { get; }
        string Login { get; set; }
        string Password { get; set; }
    }
}