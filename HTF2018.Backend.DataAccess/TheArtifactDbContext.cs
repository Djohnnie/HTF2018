using HTF2018.Backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HTF2018.Backend.DataAccess
{
    public class TheArtifactDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Statistics> Statistics { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"...");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<Team>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<Team>().Property(x => x.SysId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Challenge>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<Challenge>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<Challenge>().Property(x => x.SysId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Statistics>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<Statistics>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<Statistics>().Property(x => x.SysId).ValueGeneratedOnAdd();
        }
    }
}