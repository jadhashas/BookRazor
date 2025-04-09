using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppWASM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;

        // Injection du service IUtilisateurService dans le contrôleur
        public UtilisateurController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        // GET: api/Utilisateur
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var utilisateurs = await _utilisateurService.GetAllAsync();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateur/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var utilisateur = await _utilisateurService.GetByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound(); // 404 si l'utilisateur n'est pas trouvé
            }
            return Ok(utilisateur);
        }

        // POST: api/Utilisateur
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                var createdUtilisateur = await _utilisateurService.AddAsync(utilisateur);
                return CreatedAtAction(nameof(GetById), new { id = createdUtilisateur.Id }, createdUtilisateur);
            }
            return BadRequest(ModelState); // 400 si les données envoyées ne sont pas valides
        }

        // PUT: api/Utilisateur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest(); // 400 si l'ID dans l'URL ne correspond pas à celui de l'utilisateur
            }

            var updatedUtilisateur = await _utilisateurService.UpdateAsync(utilisateur);
            if (updatedUtilisateur == null)
            {
                return NotFound(); // 404 si l'utilisateur n'existe pas
            }
            return NoContent(); // 204 si la mise à jour réussit
        }

        // DELETE: api/Utilisateur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _utilisateurService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(); // 404 si l'utilisateur n'est pas trouvé
            }
            return NoContent(); // 204 si la suppression réussit
        }
    }
}
