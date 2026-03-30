using ApiDotNet.DTOs;
using ApiDotNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var usuario = await _authService.ValidarUsuario(dto.Email, dto.Senha);

            if (usuario == null)
                return Unauthorized("Email ou senha inválidos");

            var token = _tokenService.GerarToken(usuario);

            return Ok(new
            {
                token = token
            });
        }
    }
}