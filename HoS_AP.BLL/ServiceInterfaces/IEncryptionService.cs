namespace HoS_AP.BLL.ServiceInterfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string password);
        bool IsValidPassword(string password, string correctHash);
    }
}