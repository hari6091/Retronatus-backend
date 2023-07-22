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
    public class FeedbackController : Controller
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly RetronatusContext _context;
        public FeedbackController(ILogger<FeedbackController> logger, RetronatusContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "feedback")]
        [Authorize(Policy = "AdminOnly")]

        public ActionResult<IEnumerable<Feedback>> Get()
        {
            var feedbacks = _context.Feedback.ToList();
            if (feedbacks is null)
            {
                return NotFound();
            }

            return feedbacks;
        }

        [HttpGet("{id:int}", Name = "GetFeedback")]

        public ActionResult<Feedback> Get(int id)
        {
            var feedback = _context.Feedback
                .FirstOrDefault(l => l.IdFeedback == id);

            if (feedback is null)
            {
                return NotFound("Feedback de local não encontrado!");
            }

            return feedback;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post(Feedback feedback)
        {
            if (_context.Feedback is null)
            {
                return BadRequest(
                    "A tabela 'Feedback' não está configurada corretamente no contexto."
                );
            }

            _context.Feedback.Add(feedback);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetFeedback", new { id = feedback.IdFeedback }, feedback);
        }

        [HttpPost("ConfirmFeedback/{feedbackId}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult ConfirmFeedback(int feedbackId)
        {
            var existingFeedback = _context.Feedback.FirstOrDefault(f => f.IdFeedback == feedbackId);

            if (existingFeedback is null)
            {
                return NotFound("Feedback não encontrado.");
            }

            if (existingFeedback.Type == "Category")
            {
                Categoria categoria = new Categoria { Name = existingFeedback.Name };
                _context.Categoria.Add(categoria);
                _context.SaveChanges();

                _context.Feedback.Remove(existingFeedback);
                _context.SaveChanges();

                return Ok(categoria);
            }

            if (existingFeedback.Type == "Local")
            {
                Local local = new Local { Name = existingFeedback.Name, Address = existingFeedback.Address };
                _context.Local.Add(local);
                _context.SaveChanges();

                _context.Feedback.Remove(existingFeedback);
                _context.SaveChanges();

                return Ok(local);
            }
            return BadRequest("Tipo de feedback inválido.");
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Delete(int id)
        {
            if (_context.Feedback is null)
            {
                return BadRequest(
                    "A tabela 'Feedback' não está configurada corretamente no contexto."
                );
            }

            var feedback = _context.Feedback.FirstOrDefault(l => l.IdFeedback == id);

            if (feedback is null)
            {
                return NotFound();
            }

            _context.Feedback.Remove(feedback);
            _context.SaveChanges();

            return Ok(feedback);
        }
    }
}