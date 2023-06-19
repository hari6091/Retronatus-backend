using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        public string? Content { get; set; }

        [ForeignKey("idusuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("idpublicacao")]
        public int IdPublicacao { get; set; }
        public DateTime Date { get; set; }
        public List<Resposta>? Respostas { get; set; }
    }
}
