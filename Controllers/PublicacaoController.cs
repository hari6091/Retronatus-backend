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
            var publicacao = _context.Publicacao;
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
            var publicacao = _context.Publicacao;

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
            if (_context.Publicacao is null)
            {
                return BadRequest(
                    "A tabela 'Publicacao' não está configurada corretamente no contexto."
                );
            }

            _context.Publicacao.Add(publicacao);
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

            var publicacao = _context.Publicacao.FirstOrDefault(p => p.IdPublicacao == id);

            if (publicacao is null)
            {
                return NotFound();
            }

            _context.Publicacao.Remove(publicacao);
            _context.SaveChanges();

            return Ok(publicacao);
        }
    }
}
