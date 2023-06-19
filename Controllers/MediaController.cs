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
    public class MediaController : Controller
    {
        private readonly ILogger<MediaController> _logger;
        private readonly RetronatusContext _context;

        public MediaController(ILogger<MediaController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "media")]
        [Authorize]
        public ActionResult<IEnumerable<Media>> Get()
        {
            var medias = _context.Media;

            if (medias is null)
            {
                return NotFound();
            }

            return medias.ToList();
        }

        [HttpGet("{id:int}", Name = "GetMedia")]
        [Authorize]
        public ActionResult<Media> Get(int id)
        {
            var media = _context.Media;

            if (media is null)
            {
                return NotFound("Midia não encontrada!");
            }

            var mediaEncontrada = media.FirstOrDefault(m => m.IdMedia == id);

            if (mediaEncontrada is null)
            {
                return NotFound("Midia não encontrada!");
            }

            return mediaEncontrada;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post(Media media)
        {
            if (_context.Media is null)
            {
                return BadRequest(
                    "A tabela 'Media' não está configurada corretamente no contexto."
                );
            }

            _context.Media.Add(media);
            _context.SaveChanges();

            return new CreatedAtRouteResult(
                "GetMedia",
                new { id = media.IdMedia },
                media
            );
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Media media)
        {
            if (id != media.IdMedia)
            {
                return BadRequest();
            }

            _context.Entry(media).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(media);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (_context.Media is null)
            {
                return BadRequest(
                    "A tabela 'Media' não está configurada corretamente no contexto."
                );
            }

            var media = _context.Media.FirstOrDefault(m => m.IdMedia == id);

            if (media is null)
            {
                return NotFound();
            }

            _context.Media.Remove(media);
            _context.SaveChanges();

            return Ok(media);
        }
        
    }
}
