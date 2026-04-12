using ApiDotNet.DTOs;
using ApiDotNet.Services;
using ApiDotNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;

        public AuthController(
            AuthService authService,
            TokenService tokenService,
            AppDbContext context
        )
        {
            _authService = authService;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var usuario = await _authService.ValidarUsuario(dto.Email, dto.Senha);

            if (usuario == null)
                return Unauthorized("Email ou senha inválidos");

            var accessToken = _tokenService.GerarToken(usuario);

            var refreshToken = _tokenService.GerarRefreshToken();

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            });
        }
    }
}