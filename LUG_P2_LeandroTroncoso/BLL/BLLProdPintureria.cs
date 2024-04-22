using BE;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProdPintureria
    {
        public BLLProdPintureria()
        {
            _mpp = new MPPProdPintureria();
        }
        MPPProdPintureria _mpp;
        public List<ProductoPintureria> LeerTodo()
        {
            return _mpp.LeerTodo();
        }
        public bool Agregar(Producto producto)
        {
            return _mpp.Agregar(producto);
        }
        public bool Modificar(Producto producto,string codigo)
        {
            return _mpp.Modificar(producto,codigo);
        }
        public bool Borrar(Producto producto)
        {
            return _mpp.Borrar(producto);
        }
        public bool ExisteCodigo(string codigo, bool modif = false, ProductoPintureria prodModif = null)
        {
            return _mpp.ExisteCodigo(codigo, modif, prodModif);
        }
       
        public decimal CalcularTotal(decimal total)
        {
            return _mpp.CalcularTotal(total);
        }


    }
}
