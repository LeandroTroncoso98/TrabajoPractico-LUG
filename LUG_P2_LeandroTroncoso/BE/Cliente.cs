using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Cliente
    {
        public override string ToString()
        {
            return $"{Nombre} {Apellido} Documento:{DNI}";
        }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long DNI { get; set; }
        public string Email { get; set; }
        public ProductoContratado ProductoContratado { get; set; }
    }
}
