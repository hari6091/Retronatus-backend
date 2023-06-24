using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
            var comentario = _context.Comentario.Include(c => c.Respostas);
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
            var comentario = _context.Comentario.Include(c => c.Respostas);
            ;

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
            if (_context.Comentario is null || _context.Publicacao is null)
            {
                return BadRequest(
                    "As tabelas 'Comentario' e 'Publicacao' não estão configuradas corretamente no contexto."
                );
            }

            var publicacao = _context.Publicacao.Find(comentario.IdPublicacao);

            if (publicacao is null)
            {
                return NotFound("Publicacao não encontrada.");
            }

            _context.Comentario.Add(comentario);
            publicacao.Comentarios ??= new List<Comentario>();
            publicacao.Comentarios.Add(comentario);
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );

            var isOwner = _context.Comentario.Any(
                c => c.IdComentario == id && c.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );

            var isOwner = _context.Comentario.Any(
                c => c.IdComentario == id && c.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }

            if (_context.Comentario is null)
            {
                return BadRequest(
                    "A tabela 'Comentario' não está configurada corretamente no contexto."
                );
            }

            var comentario = _context.Comentario
                .Include(c => c.Respostas)
                .FirstOrDefault(c => c.IdComentario == id);

            if (comentario is null)
            {
                return NotFound();
            }

            _context.Resposta.RemoveRange(comentario.Respostas);

            _context.Comentario.Remove(comentario);
            _context.SaveChanges();

            return Ok(comentario);
        }
    }
}
