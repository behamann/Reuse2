using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reuse2.Models
{
    public class Anuncio
    {
        public const int NUM_ANUNCIOS_EXIBICAO = 6;

        public int anuncioID { get; set; }

        public string pessoaID { get; set; }
        public virtual ApplicationUser pessoa { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int categoriaID { get; set; }
        public virtual Categoria categoria { get; set; }

        public virtual ICollection<Interesse> interessados { get; set; }

        [DisplayName("Subcategoria")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public String subCategoria { get; set; }

        [DisplayName("Condição")]
        public String condicao { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public String titulo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public String descricao { get; set; }

        [DisplayName("URL do vídeo")]
        public string video { get; set; }

        public bool ativo { get; set; }

        [DisplayName("Tipo")]
        public string tipo { get; set; }

        public DateTime dataCriacao { get; set; }

        public string status { get; set; }

        public static List<Anuncio> getHistoricoDeTrocas(string id)
        {
            return new ApplicationDbContext().Anuncios.Take(6).ToList();
        }

        public static List<Anuncio> getAnunciosPorId(string id, int quantidade)
        {
            return new ApplicationDbContext().Anuncios.Where(a => a.pessoaID == id).Take(quantidade).ToList();
        }

        public static List<Anuncio> getUltimosAnuncios(string tipo)
        {
            return new ApplicationDbContext().Anuncios.Where(a => a.tipo == tipo).Take(6).ToList();
        }
    }
}