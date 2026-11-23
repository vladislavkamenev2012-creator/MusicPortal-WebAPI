using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Song> Songs => Set<Song>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = "admin123", Role = "Admin", IsApproved = true },
                new User { Id = 2, Username = "user1", PasswordHash = "user123", Role = "User", IsApproved = true }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Rock" },
                new Genre { Id = 2, Name = "Pop" },
                new Genre { Id = 3, Name = "Hip-Hop" }
            );

            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Name = "Queen" },
                new Artist { Id = 2, Name = "The Weeknd" },
                new Artist { Id = 3, Name = "Travis Scott" },
                new Artist { Id = 4, Name = "¥$" },
                new Artist { Id = 5, Name = "50 Cent" },
                new Artist { Id = 6, Name = "INNA" }
            );

            modelBuilder.Entity<Song>().HasData(
                new Song { Id = 1, Title = "Bohemian Rhapsody", ArtistId = 1, GenreId = 1, UserId = 1, FilePath = "/uploads/sample.mp3" },
                new Song { Id = 2, Title = "Blinding Lights", ArtistId = 2, GenreId = 2, UserId = 2, FilePath = "/uploads/sample.mp3" },
                new Song { Id = 3, Title = "FE!N (feat. Playboi Carti)", ArtistId = 3, GenreId = 3, UserId = 1, FilePath = "/uploads/4795c2af-1616-4d20-8a30-821b72f42037.mp3" },
                new Song { Id = 4, Title = "CARNIVAL", ArtistId = 4, GenreId = 3, UserId = 1, FilePath = "/uploads/9c89dfee-e2c4-4689-ac9d-2d7d1441bd2a.mp3" },
                new Song { Id = 5, Title = "Just A Lil Bit", ArtistId = 5, GenreId = 3, UserId = 1, FilePath = "/uploads/bfcbffbb-355d-4e98-9481-e7a5d20c6a7f.mp3" },
                new Song { Id = 6, Title = "Caliente", ArtistId = 6, GenreId = 2, UserId = 1, FilePath = "/uploads/353e142e-0dd1-4617-837b-3a3513ca5655.mp3" }
            );
        }
    }
}
