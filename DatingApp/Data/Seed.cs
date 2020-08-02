using System.Collections.Generic;
using System.Linq;
using DatingApp.Helpers;
using DatingApp.Models;
using Newtonsoft.Json;

namespace DatingApp.Data
{
    public static class Seed
    {
        public static void SeedUsers(DataContext ctx)
        {
            if (!ctx.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/Seed/User.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var usr in users)
                {
                    byte[] passwordHash, passwordSalt;
                    PasswordHelper.Compute("test", out passwordHash, out passwordSalt);
                    usr.PasswordHash = passwordHash;
                    usr.PasswordSalt = passwordSalt;
                }
                ctx.Users.AddRange(users);
                ctx.SaveChanges();
            }
        }
    }
}