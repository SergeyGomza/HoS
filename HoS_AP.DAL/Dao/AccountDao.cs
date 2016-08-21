using System.Linq;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.Dao
{
    public class AccountDao : FileSystemRepository, IAccountDao
    {
        public Account Load(string userName)
        {
            return Accounts.FirstOrDefault(x => x.UserName == userName);
        }
    }
}