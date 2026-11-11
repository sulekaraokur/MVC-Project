using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;

namespace APP.Domain
{
    // Creates Db instances at design time for EF tools (migrations, scaffolding) and seeding.
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        const string CONNECTIONSTRING = "data source=DB.db";

        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlite(CONNECTIONSTRING);
            return new Db(optionsBuilder.Options);
        }

        public void SeedDb()
        {
            var db = CreateDbContext(null);

            db.UserRoles.RemoveRange(db.UserRoles.ToList());
            db.Roles.RemoveRange(db.Roles.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            db.Statuses.RemoveRange(db.Statuses.ToList());

            // Resets the SQLite identity columns so IDs start from 1 again.
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='UserRoles'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Roles'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Users'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Statuses'");

            db.Roles.Add(new Role { Name = "Admin" });
            db.Roles.Add(new Role { Name = "User" });
            db.SaveChanges();

            db.Statuses.Add(new Status
            {
                Title = "Active",
                Users = new List<User>
                {
                    new User
                    {
                        UserName = "admin",
                        Password = "admin",
                        RegistrationDate = new DateTime(2026, 7, 16, 20, 57, 45),
                        UserRoles = new List<UserRole>
                        {
                            new UserRole { RoleId = db.Roles.Single(r => r.Name == "Admin").Id }
                        }
                    },
                    new User
                    {
                        UserName = "user",
                        Password = "user",
                        RegistrationDate = DateTime.Parse("07/16/2026 20:58:13", new CultureInfo("en-US")),
                        UserRoles = new List<UserRole>
                        {
                            new UserRole { RoleId = db.Roles.Single(r => r.Name == "User").Id }
                        }
                    }
                }
            });
            db.Statuses.Add(new Status { Title = "Inactive" });
            db.SaveChanges();
        }
    }
}
