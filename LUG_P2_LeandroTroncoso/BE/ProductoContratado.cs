using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductoContratado : Producto
    {
        public Cliente ClienteAsociado { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
