using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;

namespace AmisMessengerApi.Helper
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //connect tói databse
        public DataContext(DbContextOptions<DataContext> options )
            : base(options) { }
    }
}
