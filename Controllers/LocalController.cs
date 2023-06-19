using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using retronatus_backend.Context;
using retronatus_backend.Model;

namespace retronatus_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalController : Controller
    {
        private readonly ILogger<LocalController> _logger;
        private readonly RetronatusContext _context;

        public LocalController(ILogger<LocalController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "local")]
        [Authorize]
        public ActionResult<IEnumerable<Local>> Get()
        {
            var locais = _context.Local.Include(l => l.Publicacoes).ToList();
            if (locais is null)
            {
                return NotFound();
            }

            foreach (var local in locais)
            {
                local.Publicacoes ??= new List<Publicacao>();
            }

            return locais;
        }

        [HttpGet("{id:int}", Name = "GetLocal")]
        [Authorize]
        public ActionResult<Local> Get(int id)
        {
            var local = _context.Local
                .Include(l => l.Publicacoes)
                .FirstOrDefault(l => l.IdLocal == id);

            if (local is null)
            {
                return NotFound("Local não encontrado!");
            }

            return local;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Post(Local local)
        {
            if (_context.Local is null)
            {
                return BadRequest(
                    "A tabela 'Local' não está configurada corretamente no contexto."
                );
            }

            _context.Local.Add(local);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetLocal", new { id = local.IdLocal }, local);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Put(int id, Local local)
        {
            if (id != local.IdLocal)
            {
                return BadRequest("O ID fornecido não corresponde ao ID do objeto Local.");
            }

            var existingLocal = _context.Local.Find(id);

            if (existingLocal is null)
            {
                return NotFound("Local não encontrado.");
            }

            existingLocal.Name = local.Name;
            existingLocal.Address = local.Address;

            _context.SaveChanges();

            return Ok(existingLocal);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Delete(int id)
        {
            if (_context.Local is null)
            {
                return BadRequest(
                    "A tabela 'Local' não está configurada corretamente no contexto."
                );
            }

            var local = _context.Local.FirstOrDefault(l => l.IdLocal == id);

            if (local is null)
            {
                return NotFound();
            }

            _context.Local.Remove(local);
            _context.SaveChanges();

            return Ok(local);
        }
    }
}
