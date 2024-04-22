using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Producto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public Proveedor Proveedor { get; set; }
        public bool Estado { get; set; }
    }
}
