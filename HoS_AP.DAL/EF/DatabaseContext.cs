namespace HoS_AP.DAL.EF
{
    using System;
    using System.Data.Entity;

    using HoS_AP.DAL.Dto;
    using HoS_AP.Misc;

    public class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }

        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new StoreDbInitializer());
        }
        public DatabaseContext()
            : base("HeroesContext")
        {
        }

        public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
        {
            protected override void Seed(DatabaseContext db)
            {
                db.Accounts.Add(new Account { UserName = "Megan", Password = "1000:KmPsJ6b8qrf5d0flq2JZ7pZfXFiIZWfK:VeuXPEkDBL5B3rCCfE7OPVLumsX0NAJT" });

                db.SaveChanges();
            }
        }

        // unused method to include EntityFramework.SqlServerlibrary by copy in bin folder executable project
        // like http://robsneuron.blogspot.com.by/2013/11/entity-framework-upgrade-to-6.html (Copy Local was not enough)
        static void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
