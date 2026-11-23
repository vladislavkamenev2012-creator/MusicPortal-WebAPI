using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.DTOs;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;

        public AdminService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // ================= Пользователи =================

        private static UserDTO ToDto(User u) => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Role = u.Role,
            IsApproved = u.IsApproved
        };

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _uow.Users.GetAllAsync();
            return users.Select(ToDto);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            return user == null ? null : ToDto(user);
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO dto)
        {
            var username = dto.Username.Trim();
            if (await _uow.Users.Find(u => u.Username == username).AnyAsync())
                throw new InvalidOperationException($"User '{username}' already exists.");

            var user = new User
            {
                Username = username,
                PasswordHash = dto.Password,
                Role = dto.Role,
                IsApproved = dto.IsApproved
            };
            await _uow.Users.CreateAsync(user);
            await _uow.SaveChangesAsync();
            return ToDto(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UserUpdateDTO dto)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return false;

            var username = dto.Username.Trim();
            if (await _uow.Users.Find(u => u.Username == username && u.Id != id).AnyAsync())
                throw new InvalidOperationException($"User '{username}' already exists.");

            user.Username = username;
            user.Role = dto.Role;
            user.IsApproved = dto.IsApproved;
            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = dto.Password;

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task ApproveUserAsync(int userId)
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user != null)
            {
                user.IsApproved = true;
                user.Role = "User";
                _uow.Users.Update(user);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user == null) return false;

            _uow.Users.Delete(user);
            await _uow.SaveChangesAsync();
            return true;
        }

        // ================= Исполнители =================

        public async Task<IEnumerable<ArtistDTO>> GetArtistsAsync()
        {
            var artists = await _uow.Artists.GetAllAsync();
            return artists.OrderBy(a => a.Name).Select(a => new ArtistDTO { Id = a.Id, Name = a.Name });
        }

        public async Task<ArtistDTO?> GetArtistByIdAsync(int id)
        {
            var a = await _uow.Artists.GetByIdAsync(id);
            return a == null ? null : new ArtistDTO { Id = a.Id, Name = a.Name };
        }

        public async Task<ArtistDTO> AddArtistAsync(string name)
        {
            var artist = new Artist { Name = name.Trim() };
            await _uow.Artists.CreateAsync(artist);
            await _uow.SaveChangesAsync();
            return new ArtistDTO { Id = artist.Id, Name = artist.Name };
        }

        public async Task<bool> UpdateArtistAsync(int id, string name)
        {
            var artist = await _uow.Artists.GetByIdAsync(id);
            if (artist == null) return false;

            artist.Name = name.Trim();
            _uow.Artists.Update(artist);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            var artist = await _uow.Artists.GetByIdAsync(id);
            if (artist == null) return false;

            _uow.Artists.Delete(artist);
            await _uow.SaveChangesAsync();
            return true;
        }

        // ================= Жанры =================

        public async Task<IEnumerable<GenreDTO>> GetGenresAsync()
        {
            var genres = await _uow.Genres.GetAllAsync();
            return genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name });
        }

        public async Task<GenreDTO?> GetGenreByIdAsync(int id)
        {
            var g = await _uow.Genres.GetByIdAsync(id);
            return g == null ? null : new GenreDTO { Id = g.Id, Name = g.Name };
        }

        public async Task<GenreDTO> AddGenreAsync(string name)
        {
            var genre = new Genre { Name = name.Trim() };
            await _uow.Genres.CreateAsync(genre);
            await _uow.SaveChangesAsync();
            return new GenreDTO { Id = genre.Id, Name = genre.Name };
        }

        public async Task<bool> UpdateGenreAsync(int id, string name)
        {
            var genre = await _uow.Genres.GetByIdAsync(id);
            if (genre == null) return false;

            genre.Name = name.Trim();
            _uow.Genres.Update(genre);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _uow.Genres.GetByIdAsync(id);
            if (genre == null) return false;

            _uow.Genres.Delete(genre);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
