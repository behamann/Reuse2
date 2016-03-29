using System.Collections.Generic;
using System.Linq;

namespace Reuse2.Models
{
    public class Categoria
    {
        public int categoriaID { get; set; }

        public string titulo { get; set; }

        public virtual List<Subcategoria> subcategorias { get; set; }

        public static List<Categoria> getCategorias()
        {
            return new ApplicationDbContext().Categorias.ToList();
        }
    }
}