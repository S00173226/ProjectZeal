using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Geolocation> Geolocations { get; set; }

        public DatabaseContext(): base ("ProjectConnection")
        {

        }
        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }
    }
}
