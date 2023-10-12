using Microsoft.EntityFrameworkCore;
using SimpleMVC.Models;

namespace SimpleMVC.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<ActorMovie>().HasOne(a => a.Actor).WithMany(am => am.Actors_Movies)
                .HasForeignKey(a => a.ActorId);
            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies)
                .HasForeignKey(m => m.MovieId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
