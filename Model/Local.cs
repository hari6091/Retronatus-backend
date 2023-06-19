using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Local
    {
        [Key]
        public int IdLocal { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Publicacao>? Publicacoes { get; set; }
    }
}
