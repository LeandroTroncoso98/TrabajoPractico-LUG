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
    public class MPPProdPintureria : MPPProducto
    {
        public MPPProdPintureria()
        {
            _archivo = "Pintureria.xml";
        }
        private string _archivo;
        public new List<ProductoPintureria> LeerTodo()
        {
            try
            {
                List<ProductoPintureria> productos = new List<ProductoPintureria>();
                if (File.Exists(_archivo))
                {
                    XDocument xml = XDocument.Load(_archivo);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        ProductoPintureria producto = new ProductoPintureria();
                        producto.Codigo = prodXML.Element("Codigo").Value;
                        producto.Descripcion = prodXML.Element("Descripcion").Value;
                        producto.Marca = prodXML.Element("Marca").Value;
                        producto.PrecioUnitario = decimal.Parse(prodXML.Element("PrecioUnitario").Value);
                        producto.Cantidad = int.Parse(prodXML.Element("Cantidad").Value);
                        producto.Estado = bool.Parse(prodXML.Element("Estado").Value);
                        producto.Proveedor = _MppProveedor.Leer(prodXML.Element("Proveedor").Value);
                        producto.Color = prodXML.Element("Color").Value;
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
        public override bool Agregar(Producto producto)
        {
            try
            {
                if (producto is ProductoPintureria prodPintu)
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
                        new XElement("Codigo", "P" + prodPintu.Codigo.ToString()),
                        new XElement("Descripcion", prodPintu.Descripcion),
                        new XElement("Marca", prodPintu.Marca),
                        new XElement("PrecioUnitario", prodPintu.PrecioUnitario.ToString()),
                        new XElement("Cantidad", prodPintu.Cantidad.ToString()),
                        new XElement("Estado", prodPintu.Estado.ToString()),
                        new XElement("Proveedor", prodPintu.Proveedor.Codigo),
                        new XElement("Color", prodPintu.Color)
                        );
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
                if (producto is ProductoPintureria prodPintu)
                {
                    List<ProductoPintureria> productos = LeerTodo();
                    prodPintu.Codigo = "P" + prodPintu.Codigo;
                    productos = productos.Where(m => m.Codigo != codigo).ToList();
                    productos.Add(prodPintu);
                    XDocument xml = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Productos",
                        productos.Select(p => new XElement("Producto",
                        new XElement("Codigo", p.Codigo.ToString()),
                        new XElement("Descripcion", p.Descripcion),
                        new XElement("Marca", p.Marca),
                        new XElement("PrecioUnitario", p.PrecioUnitario.ToString()),
                        new XElement("Cantidad", p.Cantidad.ToString()),
                        new XElement("Estado", p.Estado.ToString()),
                        new XElement("Proveedor", p.Proveedor.Codigo),
                        new XElement("Color", p.Color)
                        ))));
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
        public override bool Borrar(Producto producto)
        {
            try
            {
                List<ProductoPintureria> productos = LeerTodo();
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
                    new XElement("Color", p.Color)
                    ))));
                xml.Save(_archivo);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ExisteCodigo(string codigo, bool modif = false, ProductoPintureria prodModif = null)
        {
            List<ProductoPintureria> productos = LeerTodo();
            if (modif)
            {
                return productos.Any(m => m.Codigo == ("P" + codigo) && m.Codigo != (prodModif.Codigo));
            }
            else
            {
                return productos.Any(m => m.Codigo == ("P" + codigo));
            }
        }
        
        
        public override decimal CalcularTotal(decimal total)
        {
            return total * 0.9m;
        }
    }
}
