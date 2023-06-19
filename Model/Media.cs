using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Media
    {
        [Key]
        public int IdMedia { get; set; }
        public string? Source { get; set; }
        public string? Type { get; set; }

        [ForeignKey("idpublicacao")]
        public int IdPublicacao { get; set; }
    }
}
