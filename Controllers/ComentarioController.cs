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
    public class ComentarioController : Controller
    {
        private readonly ILogger<ComentarioController> _logger;
        private readonly RetronatusContext _context;

        public ComentarioController(ILogger<ComentarioController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "comentario")]
        [Authorize]
        public ActionResult<IEnumerable<Comentario>> Get()
        {
            var comentario = _context.Comentario;
            if (comentario is null)
            {
                return NotFound();
            }
            return comentario.ToList();
        }

        [HttpGet("{id:int}", Name = "GetComentario")]
        [Authorize]
        public ActionResult<Comentario> Get(int id)
        {
            var comentario = _context.Comentario;

            if (comentario is null)
            {
                return NotFound("Comentario não encontrado!");
            }

            var comentarioEncontrado = comentario.FirstOrDefault(c => c.IdComentario == id);

            if (comentarioEncontrado is null)
            {
                return NotFound("Comentario não encontrado!");
            }

            return comentarioEncontrado;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post(Comentario comentario)
        {
            if (_context.Comentario is null)
            {
                return BadRequest(
                    "A tabela 'Comentario' não está configurada corretamente no contexto."
                );
            }

            _context.Comentario.Add(comentario);
            _context.SaveChanges();

            return new CreatedAtRouteResult(
                "GetComentario",
                new { id = comentario.IdComentario },
                comentario
            );
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Comentario comentario)
        {
            if (id != comentario.IdComentario)
            {
                return BadRequest();
            }

            _context.Entry(comentario).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(comentario);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (_context.Comentario is null)
            {
                return BadRequest(
                    "A tabela 'Comentario' não está configurada corretamente no contexto."
                );
            }

            var comentario = _context.Comentario.FirstOrDefault(c => c.IdComentario == id);

            if (comentario is null)
            {
                return NotFound();
            }

            _context.Comentario.Remove(comentario);
            _context.SaveChanges();

            return Ok(comentario);
        }
    }
}
