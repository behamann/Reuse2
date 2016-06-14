using System.Collections.Generic;
using System.Linq;

namespace Reuse2.Models
{
    public class TipoDeInstituicao
    {
        public int tipoDeInstituicaoID { get; set; }
        public string nome { get; set; }

        public static List<TipoDeInstituicao> getTipos()
        {
            return new ApplicationDbContext().Tipos.ToList();
        }
    }
}