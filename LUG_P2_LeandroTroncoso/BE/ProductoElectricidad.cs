using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductoElectricidad : Producto
    {
        public CategoriaEnum Categoria { get; set; }
    }
}
