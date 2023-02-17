using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBApp.DataBase
{
    public class ApplicationContext : DbContext
    {
        string _connectionString;

        public DbSet<Person> Persons => Set<Person>();
        public ApplicationContext(string connectionString = "Data Source=PersonDB.db")
        {
            this._connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
