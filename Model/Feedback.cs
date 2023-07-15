using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Feedback
    {
        [Key]
        public int IdFeedback { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Type { get; set; }

    }
}
