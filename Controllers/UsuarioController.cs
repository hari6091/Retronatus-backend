using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using retronatus_backend.Context;
using retronatus_backend.Model;

namespace retronatus_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly RetronatusContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioController(
            ILogger<UsuarioController> logger,
            RetronatusContext context,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        [HttpGet(Name = "usuario")]
        [Authorize]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var usuario = _context.Usuario.Include(u => u.Publicacoes);
            if (usuario is null)
            {
                return NotFound();
            }
            return usuario.ToList();
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<Usuario> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Usuário não identificado");
            }

            var usuario = _context.Usuario
                .Include(u => u.Publicacoes)
                .ThenInclude(p => p.Comentarios)
                .Include(u => u.Publicacoes)
                .ThenInclude(p => p.Medias)
                .FirstOrDefault(u => u.IdUsuario == int.Parse(userId));

            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }

            return usuario;
        }

        [HttpGet("{id:int}", Name = "GetUsuario")]
        [Authorize]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _context.Usuario.Include(u => u.Publicacoes);

            if (usuario is null)
            {
                return NotFound("Usuário não encontrado!");
            }

            var usuarioEncontrado = usuario.FirstOrDefault(u => u.IdUsuario == id);

            if (usuarioEncontrado is null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return usuarioEncontrado;
        }

        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {
            if (_context.Usuario is null)
            {
                return BadRequest(
                    "A tabela 'Usuario' não está configurada corretamente no contexto."
                );
            }

            if (
                string.IsNullOrEmpty(usuario.Email)
                || string.IsNullOrEmpty(usuario.Password)
                || string.IsNullOrEmpty(usuario.Name)
            )
            {
                return BadRequest("Preencha todos os campos obrigatórios!");
            }

            try
            {
                MailAddress mailAddress = new MailAddress(usuario.Email);
            }
            catch (FormatException)
            {
                return BadRequest("O email informado é inválido!");
            }

            usuario.Password = EncryptData(usuario.Password, "ysdgfhjksfasfhjgl");

            _context.Usuario.Add(usuario);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Usuario usuario)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("Usuário logado não identificado");
            }

            var isAdmin = _context.Usuario.Any(
                u => u.IdUsuario == int.Parse(userId) && u.Is_Super_Admin
            );
            var isOwner = _context.Usuario.Any(
                u => u.IdUsuario == id && u.IdUsuario == int.Parse(userId)
            );

            if (!isOwner && !isAdmin)
            {
                return Unauthorized("Você não é o proprietário dessa informação!");
            }
            ;

            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }
            ;

            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Delete(int id)
        {
            if (_context.Usuario is null)
            {
                return BadRequest(
                    "A tabela 'Usuario' não está configurada corretamente no contexto."
                );
            }

            var usuario = _context.Usuario.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario is null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpPost("login")]
        public IActionResult Login(Login model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }

            var user = AuthenticateUser(model.Email, model.Password);

            if (user != null)
            {
                var token = GenerateJwtToken(user.IdUsuario.ToString(), user.Is_Super_Admin);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new { Message = userId });
        }

        private Usuario? AuthenticateUser(string email, string password)
        {
            var users = _context.Usuario?.ToList();

            var user = users?.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                if (VerifyPassword(password, user.Password))
                {
                    return user;
                }
            }

            return null;
        }

        private string GenerateJwtToken(string userId, bool isSuperAdmin)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "")
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, isSuperAdmin ? "admin" : "user")
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(
                    Convert.ToDouble(_configuration["Jwt:ExpirationHours"])
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string EncryptData(string data, string key)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                byte[]? encryptedBytes = null;

                using (var hmac = new HMACSHA256(keyBytes))
                {
                    encryptedBytes = hmac.ComputeHash(dataBytes);
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        private bool VerifyPassword(string password, string? encryptedPassword)
        {
            if (encryptedPassword is null)
            {
                return false;
            }

            var encryptedInput = EncryptData(password, "ysdgfhjksfasfhjgl");
            return encryptedInput == encryptedPassword;
        }
    }
}
