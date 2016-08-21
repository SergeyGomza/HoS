using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HoS_AP.DAL.Dto;
using Newtonsoft.Json;

namespace HoS_AP.DAL.Dao
{
    public class FileSystemRepository
    {
        private readonly string basePath;
        private const string defaultAccount = "[{'UserName':'Megan', 'Password':'1000:KmPsJ6b8qrf5d0flq2JZ7pZfXFiIZWfK:VeuXPEkDBL5B3rCCfE7OPVLumsX0NAJT'}]";
        private readonly List<Account> accounts;

        public FileSystemRepository()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            basePath = Path.GetDirectoryName(path);

            accounts = Load<List<Account>>("Accounts.json");
        }

        protected IQueryable<Account> Accounts
        {
            get { return accounts.AsQueryable(); }
        }

        private T Load<T>(string fileName)
        {
            var path = Path.Combine(basePath, fileName);
            if (!File.Exists(path))
            {
                File.WriteAllText(path, defaultAccount);
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}