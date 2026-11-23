using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTOs
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ArtistId { get; set; }
        public string Artist { get; set; } = string.Empty; // имя исполнителя (для отображения)
        public string FilePath { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    public class GenreDTO
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }

    public class ArtistDTO
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    // Ответ API (пароль наружу не отдаётся)
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
    }

    // Тело запроса POST /api/users
    public class UserCreateDTO
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;

        [RegularExpression("^(Admin|User|Pending)$", ErrorMessage = "Role must be Admin, User or Pending.")]
        public string Role { get; set; } = "User";

        public bool IsApproved { get; set; } = true;
    }

    // Тело запроса PUT /api/users/{id}. Password необязателен: если не указан — пароль не меняется.
    public class UserUpdateDTO
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 4)]
        public string? Password { get; set; }

        [RegularExpression("^(Admin|User|Pending)$", ErrorMessage = "Role must be Admin, User or Pending.")]
        public string Role { get; set; } = "User";

        public bool IsApproved { get; set; }
    }
}
