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
        public DbSet<User> Usersystem { get; set; }
        //connect tói databse
        public DataContext(DbContextOptions<DataContext> options )
            : base(options) { }
        //connect tói databse
        //public DbSet<File> Fileimg { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Post> jobpost { get; set; }
        public DbSet<JobCare> jobcare { get; set; }
    }
}
