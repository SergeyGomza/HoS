﻿using System.Linq;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.Dao
{
    internal class AccountDao : FileSystemRepository, IAccountDao
    {
        Account IAccountDao.Load(string userName)
        {
            return Accounts.FirstOrDefault(x => x.UserName == userName);
        }
    }
}