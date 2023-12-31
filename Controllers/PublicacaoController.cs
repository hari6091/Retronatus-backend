using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger<PublicacaoController> _logger;
        private readonly RetronatusContext _context;

        public PublicacaoController(
            ILogger<PublicacaoController> logger,
            RetronatusContext context,
            IConfiguration configuration
        )
        {
            _configuration = configuration;
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

        [HttpGet("GetByLocal/{localId:int}", Name = "GetByLocal")]
        [Authorize]
        public ActionResult<IEnumerable<Publicacao>> GetByLocal(int localId)
        {
            var publicacoes = _context.Publicacao
                .Include(p => p.Comentarios)
                .Include(p => p.Medias)
                .Where(p => p.IdLocal == localId)
                .ToList();

            return publicacoes;
        }

        [HttpGet("usuario/{userId:int}", Name = "GetPublicacoesUsuario")]
        [Authorize]
        public ActionResult<IEnumerable<Publicacao>> GetPublicacoesUsuario(int userId)
        {
            var publicacoes = _context.Publicacao
                .Include(p => p.Comentarios)
                .Include(p => p.Medias)
                .Where(p => p.IdUsuario == userId)
                .ToList();

            return publicacoes;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(Publicacao publicacao)
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
                foreach (var midia in publicacao.Medias)
                {
                    var nomeMidia = Guid.NewGuid().ToString();

                    var base64String = ConvertImageToBase64(midia.Source);

                    midia.Source = base64String;

                    var linkMidia = await UploadMidiaParaStorage(base64String, nomeMidia);

                    var novaMidia = new Media
                    {
                        Type = midia.Type,
                        IdPublicacao = publicacao.IdPublicacao,
                        Source = linkMidia
                    };

                    publicacao.Medias ??= new List<Media>();
                    publicacao.Medias.Add(novaMidia);
                }

                _context.SaveChanges();
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

        private string ConvertImageToBase64(string imagePath)
        {
            try
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Erro ao converter imagem para Base64. Detalhes: " + ex.Message
                );
            }
        }

        private async Task<string> UploadMidiaParaStorage(string source, string nomeMidia)
        {
            string storageConnectionString =
                "DefaultEndpointsProtocol=https;AccountName=storageretronatus;AccountKey=71FWtm8AhYa7D9gO/wfKak0g7AsXy7eLBN2xZzFtpCfk82ItPJ/JEBItdzTm0EFOgNK0n/7mqehv+AStccF8cA==;EndpointSuffix=core.windows.net";
            string containerName = "files";

            var blobClient = new BlobClient(storageConnectionString, containerName, nomeMidia);

            using var stream = new MemoryStream(Convert.FromBase64String(source));
            await blobClient.UploadAsync(stream, overwrite: true);
            return blobClient.Uri.ToString();
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Publicacao publicacao)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );
            var isOwner = _context.Publicacao.Any(
                p => p.IdPublicacao == id && p.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );
            var isOwner = _context.Publicacao.Any(
                p => p.IdPublicacao == id && p.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }

            if (_context.Publicacao is null)
            {
                return BadRequest(
                    "A tabela 'Publicacao' não está configurada corretamente no contexto."
                );
            }

            var publicacao = _context.Publicacao
                .Include(p => p.Medias)
                .Include(p => p.Comentarios)
                .FirstOrDefault(p => p.IdPublicacao == id);

            if (publicacao is null)
            {
                return NotFound();
            }

            _context.Media.RemoveRange(publicacao.Medias);
            _context.Comentario.RemoveRange(publicacao.Comentarios);

            _context.Publicacao.Remove(publicacao);
            _context.SaveChanges();

            return Ok(publicacao);
        }
    }
}
