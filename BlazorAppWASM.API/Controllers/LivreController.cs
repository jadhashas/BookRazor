using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppWASM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivreController : ControllerBase
    {
        private readonly ILivreService _livreService;

        // Injection du service ILivreService dans le contrôleur
        public LivreController(ILivreService livreService)
        {
            _livreService = livreService;
        }

        // GET: api/Livre
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livres = await _livreService.GetAllAsync();
            return Ok(livres);
        }

        // GET: api/Livre/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livre = await _livreService.GetByIdAsync(id);
            if (livre == null)
            {
                return NotFound(); // 404 si le livre n'est pas trouvé
            }
            return Ok(livre);
        }

        // POST: api/Livre
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Livre livre)
        {
            if (ModelState.IsValid)
            {
                var createdLivre = await _livreService.AddAsync(livre);
                return CreatedAtAction(nameof(GetById), new { id = createdLivre.Id }, createdLivre);
            }
            return BadRequest(ModelState); // 400 si les données envoyées ne sont pas valides
        }

        // PUT: api/Livre/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Livre livre)
        {
            if (id != livre.Id)
            {
                return BadRequest(); // 400 si l'ID dans l'URL ne correspond pas à celui du livre
            }

            var updatedLivre = await _livreService.UpdateAsync(livre);
            if (updatedLivre == null)
            {
                return NotFound(); // 404 si le livre n'existe pas
            }
            return NoContent(); // 204 si la mise à jour réussit
        }

        // DELETE: api/Livre/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _livreService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(); // 404 si le livre n'est pas trouvé
            }
            return NoContent(); // 204 si la suppression réussit
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Aucune image n'a été fournie.");
            }

            if (!image.ContentType.StartsWith("image/"))
            {
                return BadRequest("Le fichier doit être une image.");
            }

            using var stream = image.OpenReadStream();
            var success = await _livreService.UploadImageAsync(id, stream, image.FileName);

            if (!success)
            {
                return NotFound("Livre non trouvé ou erreur lors de l'upload.");
            }

            return NoContent();
        }
    }
}
