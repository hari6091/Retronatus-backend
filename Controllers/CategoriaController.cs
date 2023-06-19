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
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly RetronatusContext _context;

        public CategoriaController(ILogger<CategoriaController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "categoria")]
        [Authorize]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categoria = _context.Categoria;
            if (categoria is null)
            {
                return NotFound();
            }
            return categoria.ToList();
        }

        [HttpGet("{id:int}", Name = "GetCategoria")]
        [Authorize]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categoria;

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }

            var categoriaEncontrada = categoria.FirstOrDefault(c => c.IdCategoria == id);

            if (categoriaEncontrada is null)
            {
                return NotFound("Categoria não encontrada!");
            }

            return categoriaEncontrada;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Post(Categoria categoria)
        {
            if (_context.Categoria is null)
            {
                return BadRequest(
                    "A tabela 'Categoria' não está configurada corretamente no contexto."
                );
            }

            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult(
                "GetCategoria",
                new { id = categoria.IdCategoria },
                categoria
            );
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Delete(int id)
        {
            if (_context.Categoria is null)
            {
                return BadRequest(
                    "A tabela 'Categoria' não está configurada corretamente no contexto."
                );
            }

            var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);

            if (categoria is null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
