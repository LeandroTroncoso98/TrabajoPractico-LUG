using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mapper
{
    public class MPPProducto
    {
        public MPPProducto()
        {
            _archivo = "Productos.xml";
            _MppProveedor = new MPPProveedor();
        }
        private string _archivo;
        protected MPPProveedor _MppProveedor;
        public List<Producto> LeerTodo()
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                if (File.Exists(_archivo))
                {
                    XDocument xml = XDocument.Load(_archivo);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        Producto producto = new Producto();
                        producto.Codigo = prodXML.Element("Codigo").Value;
                        producto.Descripcion = prodXML.Element("Descripcion").Value;
                        producto.Marca = prodXML.Element("Marca").Value;
                        producto.PrecioUnitario = decimal.Parse(prodXML.Element("PrecioUnitario").Value);
                        producto.Cantidad = int.Parse(prodXML.Element("Cantidad").Value);
                        producto.Estado = bool.Parse(prodXML.Element("Estado").Value);
                        producto.Proveedor = _MppProveedor.Leer(prodXML.Element("Proveedor").Value);
                        productos.Add(producto);
                    }
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool Agregar(Producto producto)
        {
            try
            {
                XDocument xml;
                if (File.Exists(_archivo))
                {
                    xml = XDocument.Load(_archivo);
                }
                else
                {
                    xml = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Productos"));
                }
                XElement prodXML = new XElement("Producto",
                    new XElement("Codigo", producto.Codigo.ToString()),
                    new XElement("Descripcion", producto.Descripcion),
                    new XElement("Marca", producto.Marca),
                    new XElement("PrecioUnitario", producto.PrecioUnitario.ToString()),
                    new XElement("Cantidad", producto.Cantidad.ToString()),
                    new XElement("Estado", producto.Estado.ToString()),
                    new XElement("Proveedor", producto.Proveedor.Codigo)
                    );
                xml.Element("Productos").Add(prodXML);
                xml.Save(_archivo);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool Modificar(Producto producto, string codigo)
        {
            try
            {
                List<Producto> productos = LeerTodo();
                productos = productos.Where(m => m.Codigo != producto.Codigo).ToList();
                productos.Add(producto);
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Productos",
                    productos.Select(p => new XElement("Producto",
                    new XElement("Codigo", p.Codigo.ToString()),
                    new XElement("Descripcion", p.Descripcion),
                    new XElement("Marca", p.Marca),
                    new XElement("PrecioUnitario", p.PrecioUnitario),
                    new XElement("Cantidad", p.Cantidad.ToString()),
                    new XElement("Estado", p.Estado.ToString()),
                    new XElement("Proveedor", p.Proveedor.Codigo)
                    ))));
                xml.Save(_archivo);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool Borrar(Producto producto)
        {
            try
            {
                List<Producto> productos = LeerTodo();
                productos = productos.Where(m => m.Codigo != producto.Codigo).ToList();
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Productos",
                    productos.Select(p => new XElement("Producto",
                    new XElement("Codigo", p.Codigo.ToString()),
                    new XElement("Descripcion", p.Descripcion),
                    new XElement("Marca", p.Marca),
                    new XElement("PrecioUnitario", p.PrecioUnitario),
                    new XElement("Cantidad", p.Cantidad.ToString()),
                    new XElement("Estado", p.Estado.ToString()),
                    new XElement("Proveedor", p.Proveedor.Codigo)
                    ))));
                xml.Save(_archivo);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool VerificarStock(int cantidadStock, int cantidadContratar)
        {
            if (cantidadContratar > cantidadStock) return false;
            return true;
        }
        public virtual decimal CalcularTotal(decimal total)
        {
            return total;
        }

    }
}
