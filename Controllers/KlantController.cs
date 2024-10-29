using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KlantController : Controller
    {
        private readonly AppDbContext _context;

        public KlantController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetKlanten()
        {
            var klanten = _context.Klanten.ToList();
            return Ok(klanten);
        }

        [HttpPost]
        public IActionResult AddKlant([FromBody] KlantDTO klant)
        {
            _context.Klanten.Add(klant);
            _context.SaveChanges();
            return Ok(klant);
        }
    }
}
