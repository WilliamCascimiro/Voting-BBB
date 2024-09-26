using Voting.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Voting.API.Voting.Infra.DataBase.Context
{
    public class BBBDbContext : DbContext
    {
        public BBBDbContext(DbContextOptions<BBBDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            //this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Paredao> Paredoes { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<User>().HasData(
               new User("Usuário 1", "user1@gmail.com", "1234"),
               new User("Usuário 2", "user2@gmail.com", "1234"),
               new User("Usuário 3", "user3@gmail.com", "1234"));

            modelBuilder.Entity<Paredao>().HasData(
               new Paredao(new DateTime(2024, 09, 20), new DateTime(2024, 09, 20), true));

            modelBuilder.Entity<Participant>().HasData(
               new Participant("Davi"),
               new Participant("Babu Santana"),
               new Participant("Boca Rosa")
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.AddInterceptors(new ConnectionInterceptor());
        }


    }
}
