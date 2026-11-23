using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTOs;
using MusicPortal.BLL.Interfaces;

namespace MusicPortal.WEB.Controllers.Api
{
    /// <summary>Web API: управление пользователями (только для администратора).</summary>
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public UsersController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>Получить список пользователей.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
            => Ok(await _adminService.GetUsersAsync());

        /// <summary>Получить пользователя по идентификатору.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>Добавить пользователя.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserCreateDTO dto)
        {
            try
            {
                var created = await _adminService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>Изменить пользователя.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO dto)
        {
            try
            {
                return await _adminService.UpdateUserAsync(id, dto) ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>Удалить пользователя.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
            => await _adminService.DeleteUserAsync(id) ? NoContent() : NotFound();
    }
}
