using BE;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProductoContratado
    {
        public BLLProductoContratado()
        {
            _mpp = new MPPProductoContratado();
        }
        private MPPProductoContratado _mpp;
        public List<ProductoContratado> LeerProductoContratado(Cliente cliente)
        {
            return _mpp.LeerProductoContratado(cliente);
        }
        public bool ContratarProduct(int cantidad, Cliente cliente, Producto producto)
        {
            return _mpp.ContratarProduct(cantidad, cliente, producto);
        }
        public bool ClienteAsociado(int codigo)
        {
            return _mpp.ClienteAsociado(codigo);
        }
        public bool VerificarStock(int cantidadStock, int cantidadContratada)
        {
            return _mpp.VerificarStock(cantidadStock, cantidadContratada);
        }
        public bool ProductoYaContratado(int codigoCliente, string codigoProducto )
        {
            return _mpp.ProductoYaContratado(codigoCliente ,codigoProducto);
        }
        public Dictionary<string, decimal> InformeMasContratado()
        {
            return _mpp.InformeMasContratado();
        }
        public Dictionary<string, decimal> InformeMenosContratado()
        {
            return _mpp.InformeMenosContratado();
        }
        public Dictionary<string, decimal> InformeTotal()
        {
            return _mpp.InformeTotal();
        }
    }
}
