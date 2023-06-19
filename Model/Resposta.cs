using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace retronatus_backend.Model
{
    public class Resposta
    {
        [Key]
        public int IdResposta { get; set; }

        [ForeignKey("idusuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("idcomentario")]
        public int IdComentario { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }
    }
}
