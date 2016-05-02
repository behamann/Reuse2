using System;

namespace Reuse2.Models
{
    public class Imagem
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int AnuncioId { get; set; }
        public virtual Anuncio Anuncio { get; set; }
    }
}