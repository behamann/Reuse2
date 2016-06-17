using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
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
        [Required(ErrorMessage = "Este campo é obrigatório")]        
        public int categoriaID { get; set; }

        [DisplayName("Categoria")]
        public virtual Categoria categoria { get; set; }

        public virtual ICollection<Interesse> interessados { get; set; }

        [DisplayName("Subcategoria")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public String subCategoria { get; set; }

        [DisplayName("Condição")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public String condicao { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public String titulo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public String descricao { get; set; }

        [DisplayName("URL do vídeo")]
        public string video { get; set; }

        public bool ativo { get; set; }

        [NotMapped]
        public DistanciaEntreCeps distancia { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string tipo { get; set; }

        public DateTime dataCriacao { get; set; }

        public string status { get; set; }

        public virtual ICollection<Imagem> imagens { get; set; }

        public static List<Anuncio> getHistoricoDeTrocas(string id)
        {
            return new ApplicationDbContext().Anuncios.Take(6).ToList();
        }

        public static List<Anuncio> getAnunciosPorId(string id, int quantidade)
        {
            return new ApplicationDbContext().Anuncios.Where(a => a.pessoaID == id).Take(quantidade).ToList();
        }

        public static List<Anuncio> getAnunciosComInteressadosPorId(string id)
        {
            return new ApplicationDbContext().Anuncios.Where(a => a.pessoaID == id).Where(a => a.status == "Com Interessados").ToList();
        }

        public static List<Anuncio> getUltimosAnuncios(string tipo)
        {
            return new ApplicationDbContext().Anuncios.Where(a => a.tipo == tipo).Where(a => a.ativo == true).Take(6).ToList();
        }

        public static List<string> getCidadesDosAnuncios()
        {
            var db = new ApplicationDbContext();
            var list =
               from pd in db.Anuncios
               join p in db.Users on pd.pessoaID equals p.Id
               where pd.ativo == true
               orderby p.estado, p.cidade
               select new { cidade = p.cidade + " - " + p.estado };
            var teste = list.Distinct().ToList<object>();
            var returnList = new List<string>();
            foreach(var myObject in teste)
            {
                Type myType = myObject.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    string propValue = (string)prop.GetValue(myObject, null);
                    returnList.Add(propValue);
                }
            }
            return returnList;
        }
    }
}