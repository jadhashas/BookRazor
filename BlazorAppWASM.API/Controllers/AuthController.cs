using BlazorAppWASM.API.Models;
using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppWASM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var utilisateur = new Utilisateur
            {
                Email = model.Email,
                MotDePasse = model.Password,
                NomUtilisateur = model.FirstName,
                Role = "Utilisateur"
            };

            var result = await _authService.Inscription(utilisateur);

            if (result)
                return Ok(new { message = "Inscription réussie" });
            else
                return BadRequest(new { message = "L'email ou le nom d'utilisateur existe déjà" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var (success, token) = await _authService.Connexion(model.Email, model.Password);

            if (success)
                return Ok(new { token, message = "Connexion réussie" });
            else
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var user = await _authService.GetCurrentUser(token);
                return Ok(new
                {
                    user.Id,
                    user.Email,
                    user.NomUtilisateur,
                    user.Role
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
} 