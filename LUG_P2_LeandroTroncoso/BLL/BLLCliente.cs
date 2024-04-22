using BE;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCliente
    {
        public BLLCliente()
        {
            _MPP = new MPPCliente();
        }
        private MPPCliente _MPP;
        public List<Cliente> LeerTodo()
        {
            return _MPP.LeerTodo();
        }
        public bool Agregar(Cliente cliente)
        {
            return _MPP.Agregar(cliente);
        }
        public Cliente Leer(int codigo  )
        {
            return _MPP.Leer(codigo);
        }
        public bool Borrar(Cliente cliente)
        {
            return _MPP.Borrar(cliente);
        }
        public bool Modificar(Cliente cliente)
        {
            return _MPP.Modificar(cliente);
        }
        public bool ExisteDNI(long DNI, bool modif = false, Cliente clienteModificar = null)
        {
            return _MPP.ExisteDNI(DNI, modif, clienteModificar);
        }
        public bool ExisteCodigo(int codigo, bool modif = false, Cliente clienteModificar = null)
        {
            return _MPP.ExisteCodigo(codigo, modif, clienteModificar);
        }
    }
}
