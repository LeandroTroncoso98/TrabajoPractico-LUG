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
    public class MPPProdElectricidad : MPPProducto
    {
        public MPPProdElectricidad()
        {
            _archivo = "Electricidad.xml";
        }
        private string _archivo;
        public override bool Agregar(Producto producto)
        {
            try
            {
                if (producto is ProductoElectricidad productoElectricidad)
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
                        new XElement("Codigo", "E" + productoElectricidad.Codigo.ToString()),
                        new XElement("Descripcion", productoElectricidad.Descripcion),
                        new XElement("Marca", productoElectricidad.Marca),
                        new XElement("PrecioUnitario", productoElectricidad.PrecioUnitario.ToString()),
                        new XElement("Cantidad", productoElectricidad.Cantidad.ToString()),
                        new XElement("Estado", productoElectricidad.Estado.ToString()),
                        new XElement("Proveedor", productoElectricidad.Proveedor.Codigo),
                        new XElement("Categoria", ((int)productoElectricidad.Categoria).ToString())

                        ) ;
                    xml.Element("Productos").Add(prodXML);
                    xml.Save(_archivo);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override bool Modificar(Producto producto, string codigo)
        {
            try
            {
                if(producto is ProductoElectricidad prodElectric)
                {
                    List<ProductoElectricidad> productos = LeerTodo();
                    prodElectric.Codigo = "E" + prodElectric.Codigo;
                    productos = productos.Where(m => m.Codigo != codigo).ToList();
                    productos.Add(prodElectric);
                    XDocument xml = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Productos",
                        productos.Select(p => new XElement("Producto",
                        new XElement("Codigo",p.Codigo.ToString()),
                        new XElement("Descripcion", p.Descripcion),
                        new XElement("Marca", p.Marca),
                        new XElement("PrecioUnitario", p.PrecioUnitario.ToString()),
                        new XElement("Cantidad", p.Cantidad.ToString()),
                        new XElement("Estado", p.Estado.ToString()),
                        new XElement("Proveedor", p.Proveedor.Codigo),
                        new XElement("Categoria",((int)prodElectric.Categoria).ToString())
                        ))));
                    xml.Save(_archivo);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public new List<ProductoElectricidad> LeerTodo()
        {
            try
            {
                List<ProductoElectricidad> productos = new List<ProductoElectricidad>();
                if (File.Exists(_archivo))
                {
                    XDocument xml = XDocument.Load(_archivo);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        ProductoElectricidad producto = new ProductoElectricidad();
                        producto.Codigo = prodXML.Element("Codigo").Value;
                        producto.Descripcion = prodXML.Element("Descripcion").Value;
                        producto.Marca = prodXML.Element("Marca").Value;
                        producto.PrecioUnitario = decimal.Parse(prodXML.Element("PrecioUnitario").Value);
                        producto.Cantidad = int.Parse(prodXML.Element("Cantidad").Value);
                        producto.Estado = bool.Parse(prodXML.Element("Estado").Value);
                        producto.Proveedor = _MppProveedor.Leer(prodXML.Element("Proveedor").Value);
                        producto.Categoria = (CategoriaEnum)int.Parse(prodXML.Element("Categoria").Value);
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
        public override bool Borrar(Producto producto)
        {
            try
            {
                if(producto is ProductoElectricidad prodElectric)
                {
                    List<ProductoElectricidad> productos = LeerTodo();
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
                        new XElement("Proveedor", p.Proveedor.Codigo),
                        new XElement("Categoria",((int)p.Categoria).ToString())
                        ))));
                    xml.Save(_archivo);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool ExisteCodigo(string codigo, bool modif = false, ProductoElectricidad prodModif = null)
        {
            List<ProductoElectricidad> productos = LeerTodo();
            if (modif)
            {
                return productos.Any(m => m.Codigo == ("E"+codigo) && m.Codigo != ("E"+prodModif.Codigo));
            }
            else
            {
                return productos.Any(m => m.Codigo == ("E" + codigo));
            }
        }

        public override decimal CalcularTotal(decimal total)
        {
            return total * 0.8m;
        }
    }
}
