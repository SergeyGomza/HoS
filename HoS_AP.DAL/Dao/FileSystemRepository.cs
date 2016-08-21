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
        private readonly List<Character> characters;

        public FileSystemRepository()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            basePath = Path.GetDirectoryName(path);

            accounts = Load<List<Account>>("Accounts.json", defaultAccount);
            characters = Load<List<Character>>("Characters.json", string.Empty);
        }

        protected IQueryable<Account> Accounts
        {
            get { return accounts.AsQueryable(); }
        }

        protected IQueryable<Character> Characters
        {
            get { return characters.AsQueryable(); }
        }

        private T Load<T>(string fileName, string defaultFileContent)
        {
            var path = Path.Combine(basePath, fileName);
            if (!File.Exists(path))
            {
                File.WriteAllText(path, defaultFileContent);
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}