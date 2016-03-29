using System.Collections.Generic;
using System.Linq;

namespace Reuse2.Models
{
    public class Subcategoria
    {
        public int subCategoriaID { get; set; }
        public int categoriaID { get; set; }
        public string titulo { get; set; }
        public Categoria categoria { get; set; }
    }
}