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
    public class PublicacaoController : Controller
    {
        private readonly ILogger<PublicacaoController> _logger;
        private readonly RetronatusContext _context;

        public PublicacaoController(ILogger<PublicacaoController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "publicacao")]
        [Authorize]
        public ActionResult<IEnumerable<Publicacao>> Get()
        {
            var publicacao = _context.Publicacao.Include(p => p.Comentarios).Include(p => p.Medias);
            if (publicacao is null)
            {
                return NotFound();
            }
            return publicacao.ToList();
        }

        [HttpGet("{id:int}", Name = "GetPublicacao")]
        [Authorize]
        public ActionResult<Publicacao> Get(int id)
        {
            var publicacao = _context.Publicacao.Include(p => p.Comentarios).Include(p => p.Medias);

            if (publicacao is null)
            {
                return NotFound("Publicacao não encontrada!");
            }

            var publicacaoEncontrada = publicacao.FirstOrDefault(p => p.IdPublicacao == id);

            if (publicacaoEncontrada is null)
            {
                return NotFound("Publicacao não encontrada!");
            }

            return publicacaoEncontrada;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post(Publicacao publicacao)
        {
            if (
                publicacao is null
                || _context.Usuario is null
                || _context.Local is null
                || _context.Categoria is null
            )
            {
                return BadRequest();
            }

            if (publicacao.Medias != null && publicacao.Medias.Any())
            {
                foreach (var media in publicacao.Medias)
                {
                    media.IdPublicacao = publicacao.IdPublicacao;
                    _context.Media.Add(media);
                }

                _context.SaveChanges();

                publicacao.Medias = publicacao.Medias;
            }

            _context.Publicacao.Add(publicacao);

            var usuario = _context.Usuario.Find(publicacao.IdUsuario);
            var local = _context.Local.Find(publicacao.IdLocal);
            var categoria = _context.Categoria.Find(publicacao.IdCategoria);

            if (usuario is not null)
            {
                usuario.Publicacoes ??= new List<Publicacao>();
                usuario.Publicacoes.Add(publicacao);
            }

            if (local is not null)
            {
                local.Publicacoes ??= new List<Publicacao>();
                local.Publicacoes.Add(publicacao);
            }

            if (categoria is not null)
            {
                categoria.Publicacoes ??= new List<Publicacao>();
                categoria.Publicacoes.Add(publicacao);
            }

            _context.SaveChanges();

            return new CreatedAtRouteResult(
                "GetPublicacao",
                new { id = publicacao.IdPublicacao },
                publicacao
            );
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Publicacao publicacao)
        {
            if (id != publicacao.IdPublicacao)
            {
                return BadRequest();
            }

            _context.Entry(publicacao).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(publicacao);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (_context.Publicacao is null)
            {
                return BadRequest(
                    "A tabela 'Publicacao' não está configurada corretamente no contexto."
                );
            }

            var publicacao = _context.Publicacao
                .Include(p => p.Medias)
                .FirstOrDefault(p => p.IdPublicacao == id);
                
            if (publicacao is null)
            {
                return NotFound();
            }

            _context.Media.RemoveRange(publicacao.Medias);

            _context.Publicacao.Remove(publicacao);
            _context.SaveChanges();

            return Ok(publicacao);
        }
    }
}
