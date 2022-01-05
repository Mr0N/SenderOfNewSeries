using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Message.Model
{
    internal class Context:DbContext
    {
        public DbSet<Info> infos { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public Context(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
