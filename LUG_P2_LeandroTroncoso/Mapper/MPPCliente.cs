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
    public class MPPCliente
    {
        public MPPCliente()
        {
            _archivocli = "Clientes.xml";
        }
        private string _archivocli;
        private MPPProductoContratado _mppContratado;
        public List<Cliente> LeerTodo()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();
                if (File.Exists(_archivocli))
                {
                    _mppContratado = new MPPProductoContratado();
                    XDocument xml = XDocument.Load(_archivocli);
                    foreach (XElement cliXML in xml.Descendants("Cliente"))
                    {
                        Cliente cliente = new Cliente();
                        cliente.Codigo = int.Parse(cliXML.Element("Codigo").Value);
                        cliente.Nombre = cliXML.Element("Nombre").Value;
                        cliente.Apellido = cliXML.Element("Apellido").Value;
                        cliente.DNI = long.Parse(cliXML.Element("DNI").Value);
                        cliente.Email = cliXML.Element("Email").Value;
                        cliente.ProductoContratado = new ProductoContratado();
                        clientes.Add(cliente);
                    }
                }
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Agregar(Cliente cliente)
        {
            try
            {
                XDocument xml;
                if (File.Exists(_archivocli))
                {
                    xml = XDocument.Load(_archivocli);
                }
                else
                {
                    xml = new XDocument(
                        new XDeclaration("1.0","utf-8","yes"),
                        new XElement("Clientes")
                        );
                }
                XElement cliXML = new XElement("Cliente",
                    new XElement("Codigo", cliente.Codigo.ToString()),
                    new XElement("Nombre", cliente.Nombre.Trim()),
                    new XElement("Apellido", cliente.Apellido.Trim()),
                    new XElement("DNI", cliente.DNI.ToString()),
                    new XElement("Email", cliente.Email.Trim())
                    );
                xml.Element("Clientes").Add(cliXML);
                xml.Save(_archivocli);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Cliente Leer(int codigo)
        {
            try
            {
                if (File.Exists(_archivocli))
                {
                    XDocument xml = XDocument.Load(_archivocli);
                    List<Cliente> clientes = LeerTodo();
                    Cliente cliente = clientes.FirstOrDefault(m => m.Codigo == codigo);
                    return cliente;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Borrar(Cliente cliente)
        {
            try
            {
                List<Cliente> clientes = LeerTodo();
                clientes = clientes.Where(m => m.DNI != cliente.DNI).ToList();
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Clientes",
                        clientes.Select(cl => new XElement("Cliente",
                        new XElement("Codigo", cl.Codigo.ToString()),
                        new XElement("Nombre", cl.Nombre),
                        new XElement("Apellido", cl.Apellido),
                        new XElement("DNI", cl.DNI.ToString()),
                        new XElement("Email", cl.Email)
                        ))));
                xml.Save(_archivocli);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Modificar(Cliente cliente)
        {
            try
            {
                List<Cliente> clientes = LeerTodo();
                Cliente clienteMod = clientes.FirstOrDefault(m => m.DNI == cliente.DNI);
                clientes.Remove(clienteMod);
                clientes.Add(cliente);
                XDocument xml = new XDocument(

                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Clientes",
                            clientes.Select(cl => new XElement("Cliente",
                                new XElement("Codigo", cl.Codigo.ToString()),
                                new XElement("Nombre", cl.Nombre),
                                new XElement("Apellido", cl.Apellido),
                                new XElement("DNI", cl.DNI.ToString()),
                                new XElement("Email", cl.Email)
                                ))));
                xml.Save(_archivocli);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ExisteDNI(long DNI, bool modif = false, Cliente clienteModificar = null)
        {
            List<Cliente> clientes = LeerTodo();
            if (modif)
            {
                return clientes.Any(m => m.DNI == DNI && m.DNI != clienteModificar.DNI);
            }
            else
            {
                return clientes.Any(m => m.DNI == DNI);
            }
        }
        public bool ExisteCodigo(int codigo, bool modif = false,Cliente clienteModificar = null)
        {
            List<Cliente> clientes = LeerTodo();
            if (modif)
            {
                return clientes.Any(m => m.Codigo == codigo && m.Codigo != clienteModificar.Codigo);
            }
            else
            {
                return clientes.Any(m => m.Codigo == codigo);
            }
        }
    }
}
