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
    public class MPPProductoContratado : MPPProducto
    {
        public MPPProductoContratado()
        {
            _archivoContra = "Contratado.xml";
            _mppProveedor = new MPPProveedor();
            _mppCliente = new MPPCliente();
        }
        string _archivoContra;
        MPPProveedor _mppProveedor;
        MPPCliente _mppCliente;
        MPPProdElectricidad _mppElec;
        MPPProdPintureria _mppPint;
        public bool ContratarProduct(int cantidad, Cliente cliente, Producto producto)
        {
            try
            {
                XDocument xml;
                if (File.Exists(_archivoContra))
                {
                    xml = XDocument.Load(_archivoContra);
                }
                else
                {
                    xml = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Contratados"));
                }
                ProductoContratado contratado = new ProductoContratado();
                contratado.PrecioTotal = producto.PrecioUnitario * cantidad;
                if (producto is ProductoElectricidad prodElect)
                {
                    _mppElec = new MPPProdElectricidad();
                    decimal total = _mppElec.CalcularTotal(contratado.PrecioTotal);
                    XElement prodXML = new XElement("Producto",
                        new XElement("Codigo", producto.Codigo),
                        new XElement("Descripcion", producto.Descripcion),
                        new XElement("Marca", producto.Marca),
                        new XElement("PrecioUnitario", producto.PrecioUnitario.ToString()),
                        new XElement("Cantidad", producto.Cantidad.ToString()),
                        new XElement("Proveedor", producto.Proveedor.Codigo),
                        new XElement("PrecioTotal", total.ToString()),
                        new XElement("Estado", producto.Estado.ToString()),
                        new XElement("TipoProducto", "Electricidad"),
                        new XElement("ClienteCodigo", cliente.Codigo.ToString()));
                    xml.Element("Contratados").Add(prodXML);

                }
                else if (producto is ProductoPintureria prodPint)
                {
                    _mppPint = new MPPProdPintureria();
                    decimal total = _mppPint.CalcularTotal(contratado.PrecioTotal);
                    XElement prodXML = new XElement("Producto",
                        new XElement("Codigo", producto.Codigo),
                        new XElement("Descripcion", producto.Descripcion),
                        new XElement("Marca", producto.Marca),
                        new XElement("PrecioUnitario", producto.PrecioUnitario.ToString()),
                        new XElement("Cantidad", producto.Cantidad.ToString()),
                        new XElement("Proveedor", producto.Proveedor.Codigo),
                        new XElement("PrecioTotal", total.ToString()),
                        new XElement("Estado", producto.Estado.ToString()),
                        new XElement("TipoProducto", "Pintura"),
                        new XElement("ClienteCodigo", cliente.Codigo.ToString()));
                    xml.Element("Contratados").Add(prodXML);
                }
                xml.Save(_archivoContra);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public new List<ProductoContratado> LeerTodo()
        {
            try
            {
                List<ProductoContratado> productos = new List<ProductoContratado>();

                if (File.Exists(_archivoContra))
                {
                    XDocument xml = XDocument.Load(_archivoContra);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        ProductoContratado producto = new ProductoContratado();
                        producto.Codigo = prodXML.Element("Codigo").Value;
                        producto.Descripcion = prodXML.Element("Descripcion").Value;
                        producto.Marca = prodXML.Element("Marca").Value;
                        producto.PrecioUnitario = decimal.Parse(prodXML.Element("PrecioUnitario").Value);
                        producto.Cantidad = int.Parse(prodXML.Element("Cantidad").Value);
                        producto.Estado = bool.Parse(prodXML.Element("Estado").Value);
                        string codigoProv = prodXML.Element("Proveedor").Value;
                        producto.Proveedor = _mppProveedor.Leer(codigoProv);
                        producto.ClienteAsociado = _mppCliente.Leer(int.Parse(prodXML.Element("ClienteCodigo").Value));
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
        public List<ProductoContratado> LeerProductoContratado(Cliente cliente)
        {
            try
            {
                List<ProductoContratado> productos = new List<ProductoContratado>();

                if (File.Exists(_archivoContra))
                {
                    XDocument xml = XDocument.Load(_archivoContra);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        if (int.Parse(prodXML.Element("ClienteCodigo").Value) == cliente.Codigo)
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.Codigo = prodXML.Element("Codigo").Value;
                            producto.Descripcion = prodXML.Element("Descripcion").Value;
                            producto.Marca = prodXML.Element("Marca").Value;
                            producto.PrecioUnitario = decimal.Parse(prodXML.Element("PrecioUnitario").Value);
                            producto.Cantidad = int.Parse(prodXML.Element("Cantidad").Value);
                            producto.Estado = bool.Parse(prodXML.Element("Estado").Value);
                            string codigoProv = prodXML.Element("Proveedor").Value;
                            producto.Proveedor = _mppProveedor.Leer(codigoProv);
                            producto.ClienteAsociado = _mppCliente.Leer(int.Parse(prodXML.Element("ClienteCodigo").Value));
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productos.Add(producto);
                            return productos;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ClienteAsociado(int codigo)
        {
            List<ProductoContratado> productos = LeerTodo();
            return productos.Any(m => m.ClienteAsociado.Codigo == codigo);
        }
        public bool ProductoYaContratado(int codigoCliente,string codigo)
        {
            try
            {
                List<ProductoContratado> productos = LeerTodo();
                return productos.Any(m => m.ClienteAsociado.Codigo == codigoCliente || m.Codigo == codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region informe

        public Dictionary<string, decimal> InformeMasContratado()
        {
            try
            {
                List<ProductoContratado> productosElec = new List<ProductoContratado>();
                List<ProductoContratado> productosPint = new List<ProductoContratado>();
                decimal montoElec = 0;
                decimal montoPint = 0;
                Dictionary<string, decimal> masContratado = new Dictionary<string, decimal>();
                if (File.Exists(_archivoContra))
                {
                    XDocument xml = XDocument.Load(_archivoContra);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        if (prodXML.Element("TipoProducto").Value == "Pintura")
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productosPint.Add(producto);
                        }
                        else
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productosElec.Add(producto);
                        }
                    }
                    if (productosElec.Count() > productosPint.Count())
                    {
                        foreach (ProductoContratado producto in productosElec)
                        {
                            montoElec += producto.PrecioTotal;
                        }
                        masContratado.Add("Electricidad", montoElec);
                    }
                    if (productosPint.Count() > productosElec.Count())
                    {
                        foreach (ProductoContratado producto in productosPint)
                        {
                            montoPint += producto.PrecioTotal;
                        }
                        masContratado.Add("Pintura", montoPint);
                    }
                }
                return masContratado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Dictionary<string, decimal> InformeMenosContratado()
        {
            try
            {
                List<ProductoContratado> productosElec = new List<ProductoContratado>();
                List<ProductoContratado> productosPint = new List<ProductoContratado>();
                decimal montoElec = 0;
                decimal montoPint = 0;
                Dictionary<string, decimal> menosContratado = new Dictionary<string, decimal>();
                if (File.Exists(_archivoContra))
                {
                    XDocument xml = XDocument.Load(_archivoContra);
                    foreach (XElement prodXML in xml.Descendants("Producto"))
                    {
                        if (prodXML.Element("TipoProducto").Value == "Pintura")
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productosPint.Add(producto);
                        }
                        else
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productosElec.Add(producto);
                        }
                    }
                    if (productosElec.Count() < productosPint.Count())
                    {
                        foreach (ProductoContratado producto in productosElec)
                        {
                            montoElec += producto.PrecioTotal;
                        }
                        menosContratado.Add("Electricidad", montoElec);
                    }
                    if (productosPint.Count() < productosElec.Count())
                    {
                        foreach (ProductoContratado producto in productosPint)
                        {
                            montoPint += producto.PrecioTotal;
                        }
                        menosContratado.Add("Pintura", montoPint);
                    }
                }
                return menosContratado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, decimal> InformeTotal()
        {
            try
            {
                List<ProductoContratado> productosElec = new List<ProductoContratado>();
                List<ProductoContratado> productosPint = new List<ProductoContratado>();
                decimal montoElec = 0;
                decimal montoPint = 0;
                Dictionary<string, decimal> totalcontratos = new Dictionary<string, decimal>();
                if (File.Exists(_archivoContra))
                {
                    XDocument xml = XDocument.Load(_archivoContra);
                    foreach(XElement prodXML in xml.Descendants("Producto"))
                    {
                        if(prodXML.Element("TipoProducto").Value == "Pintura")
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productosPint.Add(producto);
                        }
                        else
                        {
                            ProductoContratado producto = new ProductoContratado();
                            producto.PrecioTotal = decimal.Parse(prodXML.Element("PrecioTotal").Value);
                            productosElec.Add(producto);
                        }
                    }
                    foreach(ProductoContratado producto in productosPint)
                    {
                        montoPint += producto.PrecioTotal;
                    }
                    foreach(ProductoContratado producto in productosElec)
                    {
                        montoElec += producto.PrecioTotal;
                    }
                    totalcontratos.Add("Pintura", montoPint);
                    totalcontratos.Add("Electricidad", montoElec);
                }
                return totalcontratos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
