using Entities;
using Microsoft.EntityFrameworkCore;


namespace Repository
{
    public class ATContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<State> States { get; set; }

        public ATContext(DbContextOptions<ATContext> options) :
        base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ATContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}