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
            var local = _context.Local;
            if (local is null)
            {
                return NotFound();
            }
            return local.ToList();
        }

        [HttpGet("{id:int}", Name = "GetLocal")]
        [Authorize]
        public ActionResult<Local> Get(int id)
        {
            var local = _context.Local;

            if (local is null)
            {
                return NotFound("Local não encontrado!");
            }

            var localEncontrado = local.FirstOrDefault(l => l.IdLocal == id);

            if (localEncontrado is null)
            {
                return NotFound("Local não encontrado!");
            }

            return localEncontrado;
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
                return BadRequest();
            }

            _context.Entry(local).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(local);
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
