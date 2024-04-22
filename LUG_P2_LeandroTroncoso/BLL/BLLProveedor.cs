using BE;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProveedor
    {
        public BLLProveedor()
        {
            _mpp = new MPPProveedor();
        }
        MPPProveedor _mpp;
        public bool CrearProveedoresXML()
        {
            return _mpp.CrearProveedoresXML();
        }
        public Proveedor Leer(string codigo)
        {
            return _mpp.Leer(codigo);
        }
        public List<Proveedor> LeerTodos()
        {
            return _mpp.LeerTodos();
        }
    }
}
