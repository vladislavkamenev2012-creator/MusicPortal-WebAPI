using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTOs;
using MusicPortal.BLL.Interfaces;

namespace MusicPortal.WEB.Controllers.Api
{
    /// <summary>Web API: управление жанрами песен (только для администратора).</summary>
    [ApiController]
    [Route("api/genres")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public GenresController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>Получить список жанров.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAll()
            => Ok(await _adminService.GetGenresAsync());

        /// <summary>Получить жанр по идентификатору.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDTO>> GetById(int id)
        {
            var genre = await _adminService.GetGenreByIdAsync(id);
            return genre == null ? NotFound() : Ok(genre);
        }

        /// <summary>Добавить жанр.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GenreDTO>> Create([FromBody] GenreDTO dto)
        {
            var created = await _adminService.AddGenreAsync(dto.Name);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Изменить жанр.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] GenreDTO dto)
            => await _adminService.UpdateGenreAsync(id, dto.Name) ? NoContent() : NotFound();

        /// <summary>Удалить жанр (вместе с песнями этого жанра).</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
            => await _adminService.DeleteGenreAsync(id) ? NoContent() : NotFound();
    }
}
