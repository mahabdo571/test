using BUS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
     

        public AuthController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Data");
            }

            if (model is null)
                return BadRequest("Model is Null");

            try
            {

                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return CreatedAtAction("Register", result);

                return BadRequest(result);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error .  {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Data");
            }

            if (model is null)
                return BadRequest("Model is Null");

            try
            {

                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error .  {ex.Message}");
            }
        }

    }
}
