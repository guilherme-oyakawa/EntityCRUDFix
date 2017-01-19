using NewGameStore.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NewGameStore.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreContext")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<ESRB> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}