using MusicPortal.BLL.DTOs;

namespace MusicPortal.BLL.Interfaces
{
    public interface ISongService
    {
        Task<(IEnumerable<SongDTO> Items, int TotalCount)> GetSongsAsync(string? search, int? genreId, string? sortOrder, int page, int pageSize);
        Task<SongDTO?> GetSongByIdAsync(int id);
        Task AddSongAsync(SongDTO dto);
        Task DeleteSongAsync(int id);
    }

    public interface IAdminService
    {
        // ----- Пользователи -----
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(UserCreateDTO dto);
        Task<bool> UpdateUserAsync(int id, UserUpdateDTO dto);
        Task ApproveUserAsync(int userId);
        Task<bool> DeleteUserAsync(int userId);

        // ----- Исполнители -----
        Task<IEnumerable<ArtistDTO>> GetArtistsAsync();
        Task<ArtistDTO?> GetArtistByIdAsync(int id);
        Task<ArtistDTO> AddArtistAsync(string name);
        Task<bool> UpdateArtistAsync(int id, string name);
        Task<bool> DeleteArtistAsync(int id);

        // ----- Жанры -----
        Task<IEnumerable<GenreDTO>> GetGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(int id);
        Task<GenreDTO> AddGenreAsync(string name);
        Task<bool> UpdateGenreAsync(int id, string name);
        Task<bool> DeleteGenreAsync(int id);
    }
}
