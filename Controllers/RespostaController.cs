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
    public class RespostaController : Controller
    {
        private readonly ILogger<RespostaController> _logger;
        private readonly RetronatusContext _context;

        public RespostaController(ILogger<RespostaController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Resposta")]
        [Authorize]
        public ActionResult<IEnumerable<Resposta>> Get()
        {
            var resposta = _context.Resposta;
            if (resposta is null)
            {
                return NotFound();
            }
            return resposta.ToList();
        }

        [HttpGet("{id:int}", Name = "GetResposta")]
        [Authorize]
        public ActionResult<Resposta> Get(int id)
        {
            var resposta = _context.Resposta;

            if (resposta is null)
            {
                return NotFound("Resposta não encontrada!");
            }

            var respostaEncontrada = resposta.FirstOrDefault(r => r.IdResposta == id);

            if (respostaEncontrada is null)
            {
                return NotFound("Resposta não encontrada!");
            }

            return respostaEncontrada;
        }

        [HttpGet("GetByComentario/{comentarioId:int}", Name = "GetByComentario")]
        [Authorize]
        public ActionResult<IEnumerable<Resposta>> GetByComentario(int comentarioId)
        {
            var respostas = _context.Resposta.Where(r => r.IdComentario == comentarioId).ToList();

            return respostas;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Resposta> Post(Resposta resposta)
        {
            if (_context.Resposta is null)
            {
                return BadRequest(
                    "A tabela 'Resposta' não está configurada corretamente no contexto."
                );
            }

            _context.Resposta.Add(resposta);
            var comentario = _context.Comentario.Find(resposta.IdComentario);

            if (comentario is not null)
            {
                comentario.Respostas ??= new List<Resposta>();
                comentario.Respostas.Add(resposta);
            }

            _context.SaveChanges();

            return new CreatedAtRouteResult(
                "GetResposta",
                new { id = resposta.IdResposta },
                resposta
            );
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Resposta resposta)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );
            var isOwner = _context.Resposta.Any(
                r => r.IdResposta == id && r.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }
            ;

            if (id != resposta.IdResposta)
            {
                return BadRequest();
            }

            _context.Entry(resposta).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(resposta);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (_context.Resposta is null || _context.Usuario is null)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );
            var isOwner = _context.Resposta.Any(
                r => r.IdResposta == id && r.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }

            var resposta = _context.Resposta.FirstOrDefault(l => l.IdResposta == id);

            if (resposta is null)
            {
                return NotFound();
            }

            _context.Resposta.Remove(resposta);
            _context.SaveChanges();

            return Ok(resposta);
        }
    }
}
