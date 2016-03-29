using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reuse2.Models
{
    public class Interesse
    {
        public string userID { get; set; }
        public int anuncioID { get; set; }

        public string texto { get; set; }
        public bool? aceito { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Anuncio> Anuncios { get; set; }

        public static List<Interesse> getInteressesByUser(string id)
        {
            var db = new ApplicationDbContext();
            //Recupera os interesses que o usuário possui (anuncios que propos a troca)
            var interesses = db.Interesses.Where(i => i.userID == id).ToList();
            //Para cada interesse, recuperar o anuncio e usuario relacionado
            foreach (var item in interesses)
            {
                item.Anuncios = db.Anuncios.Where(a => a.anuncioID == item.anuncioID).ToList();
                item.Users = db.Users.Where(u => u.Id == item.userID).ToList();
            }
            return interesses;
        }

        public static List<Interesse> getInteressesByAnuncio(int id)
        {
            var db = new ApplicationDbContext();
            //Recupera os interesses que o usuário possui (anuncios que propos a troca)
            var interesses = db.Interesses.Where(i => i.anuncioID == id).ToList();
            //Para cada interesse, recuperar o anuncio e usuario relacionado
            foreach (var item in interesses)
            {
                item.Anuncios = db.Anuncios.Where(a => a.anuncioID == item.anuncioID).ToList();
                item.Users = db.Users.Where(u => u.Id == item.userID).ToList();
            }
            return interesses;
        }
    }
}