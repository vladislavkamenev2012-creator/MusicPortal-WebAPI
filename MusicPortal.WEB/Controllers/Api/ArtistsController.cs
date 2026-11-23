using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTOs;
using MusicPortal.BLL.Interfaces;

namespace MusicPortal.WEB.Controllers.Api
{
    /// <summary>Web API: управление исполнителями песен (только для администратора).</summary>
    [ApiController]
    [Route("api/artists")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class ArtistsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public ArtistsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>Получить список исполнителей.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAll()
            => Ok(await _adminService.GetArtistsAsync());

        /// <summary>Получить исполнителя по идентификатору.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDTO>> GetById(int id)
        {
            var artist = await _adminService.GetArtistByIdAsync(id);
            return artist == null ? NotFound() : Ok(artist);
        }

        /// <summary>Добавить исполнителя.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ArtistDTO>> Create([FromBody] ArtistDTO dto)
        {
            var created = await _adminService.AddArtistAsync(dto.Name);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Изменить исполнителя.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ArtistDTO dto)
            => await _adminService.UpdateArtistAsync(id, dto.Name) ? NoContent() : NotFound();

        /// <summary>Удалить исполнителя (вместе с его песнями).</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
            => await _adminService.DeleteArtistAsync(id) ? NoContent() : NotFound();
    }
}
