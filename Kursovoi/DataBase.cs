using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
namespace Kursovoi
{
    internal class DataBase
    {
        public class FileData
        {
            public int Id { get; set; }
            public string FilName { get; set; }
            public byte[] Fil { get; set; }
            public string Ext { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }
        }
        public class Roles
        {
            public int RoleId { get; set; }
            public string RoleName {  get; set; }
        }
        public class AppDbContext : DbContext
        {
            public DbSet<FileData> FileData { get; set; }
            public DbSet<User> User { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=kyserv;Database=DestFile;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
    }
}