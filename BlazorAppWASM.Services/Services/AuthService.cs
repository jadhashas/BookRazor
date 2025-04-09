using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlazorAppWASM.DAL.Data;
using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlazorAppWASM.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DbContexte _context;

        public AuthService(IConfiguration configuration, DbContexte context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<bool> Inscription(Utilisateur utilisateur)
        {
            try
            {
                utilisateur.MotDePasse = HashPassword(utilisateur.MotDePasse);
                await _context.Set<Utilisateur>().AddAsync(utilisateur);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(bool success, string token)> Connexion(string email, string motDePasse)
        {
            var utilisateur = await _context.Set<Utilisateur>()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (utilisateur == null || !VerifyPassword(motDePasse, utilisateur.MotDePasse))
            {
                return (false, null);
            }

            var token = GenerateJwtToken(utilisateur);
            return (true, token);
        }

        public async Task<Utilisateur> GetCurrentUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return await _context.Set<Utilisateur>()
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        }

        private string GenerateJwtToken(Utilisateur utilisateur)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
                new Claim(ClaimTypes.Email, utilisateur.Email),
                new Claim(ClaimTypes.Role, utilisateur.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
} 