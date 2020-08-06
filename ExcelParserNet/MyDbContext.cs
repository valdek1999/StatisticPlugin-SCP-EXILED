using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticPlugin
{
    public class MyDbContext:DbContext
    {
        public MyDbContext():base("DbConnectionString")
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}
