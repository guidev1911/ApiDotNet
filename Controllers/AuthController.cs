using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(
            AuthService authService,
            TokenService tokenService
        )
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var usuario = await _authService.ValidarUsuario(dto.Email, dto.Senha);

            var accessToken = _tokenService.GerarToken(usuario);
            var refreshToken = _tokenService.GerarRefreshToken();

            await _authService.AtualizarRefreshToken(usuario, refreshToken);

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenDTO dto)
        {
            var usuario = await _authService.BuscarPorRefreshToken(dto.RefreshToken);

            if (usuario == null || usuario.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Refresh token inválido");

            var newAccessToken = _tokenService.GerarToken(usuario);
            var newRefreshToken = _tokenService.GerarRefreshToken();

            await _authService.AtualizarRefreshToken(usuario, newRefreshToken);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}