using AutosChallenge.Core;
using AutosChallenge.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutosChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasAutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarcasAutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetMarcasAutos()
        {
            var marcas = await _context.MarcasAutos.ToListAsync();
            
            if (marcas == null || !marcas.Any())
            {
                return NoContent();
            }

            return Ok(marcas);
        }
    }
}
