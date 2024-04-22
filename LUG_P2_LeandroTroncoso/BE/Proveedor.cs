using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Proveedor
    {
        public override string ToString()
        {
            return RazonSocial;
        }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public long CUIT { get; set; }
    }
}
