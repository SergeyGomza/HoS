namespace HoS_AP.DAL.ServiceInterfaces
{
    public interface IAccountService
    {
        bool Authenticate(string username, string password);
    }
}