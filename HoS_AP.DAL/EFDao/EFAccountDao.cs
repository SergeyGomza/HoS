using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoS_AP.DAL.EFDao
{
    using HoS_AP.DAL.DaoInterfaces;
    using HoS_AP.DAL.Dto;
    using HoS_AP.DAL.EF;

    public class EFAccountDao : IAccountDao
    {
        Account IAccountDao.Load(string userName)
        {
            using (var context = new DatabaseContext())
            {
                // Perform data access using the context
                return context.Accounts.FirstOrDefault(x => x.UserName == userName);  
            }
        }
    }
}
