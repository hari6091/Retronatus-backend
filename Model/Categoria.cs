using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? Name { get; set; }
        public List<Publicacao>? Publicacoes { get; set; }
    }
}
