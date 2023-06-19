using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Publicacao
    {
        [Key]
        public int IdPublicacao { get; set; }
        public string? Content { get; set; }
        public List<Media>? Medias { get; set; }
        public string? Status { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("idusuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("idlocal")]
        public int IdLocal { get; set; }

        [ForeignKey("idcategoria")]
        public int IdCategoria { get; set; }

        public List<Comentario>? Comentarios { get; set; }
    }
}
