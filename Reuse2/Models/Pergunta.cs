using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reuse2.Models
{
    public class Pergunta
    {
        public int perguntaID { get; set; }
        public string descricao { get; set; }
        public string questionadorId { get; set; }
        public virtual ApplicationUser questionador { get; set; }
        public int AnuncioId { get; set; }
        public virtual Anuncio anuncio { get; set; }
        public DateTime dataDeCriacao { get; set; }
        public string resposta { get; set; }
    }
}