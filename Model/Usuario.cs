using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Is_Super_Admin { get; set; }
        public List<Publicacao>? Publicacoes { get; set; }
    }
}
