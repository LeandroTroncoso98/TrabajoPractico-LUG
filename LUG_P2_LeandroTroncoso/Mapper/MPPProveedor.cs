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
    public class MPPProveedor
    {
        public MPPProveedor()
        {
            _archivoProv = "Proveedores.xml";
        }
        private string _archivoProv;
        public List<Proveedor> LeerTodos()
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            if (File.Exists(_archivoProv))
            {
                XDocument xml = XDocument.Load(_archivoProv);
                foreach(XElement provXML in xml.Descendants("Proveedor"))
                {
                    Proveedor prov = new Proveedor();
                    prov.Codigo = provXML.Element("Codigo").Value;
                    prov.RazonSocial = provXML.Element("RazonSocial").Value;
                    prov.CUIT = long.Parse(provXML.Element("CUIT").Value);
                    proveedores.Add(prov);
                }
            }
            return proveedores;
        }
        public Proveedor Leer(string codigo)
        {
            List<Proveedor> proveedores = LeerTodos();
            Proveedor proveedor = proveedores.FirstOrDefault(m => m.Codigo == codigo);
            return proveedor;
        }
        public bool CrearProveedoresXML()
        {
            if (File.Exists(_archivoProv)) return false;
            List<Proveedor> proveedores = new List<Proveedor>();
            Proveedor prov1 = new Proveedor { Codigo = "P001", RazonSocial = "Pinturas y Revestimientos S.A.", CUIT = 30111111119 };
            Proveedor prov2 = new Proveedor { Codigo = "P002", RazonSocial = "ColorExpress Pinturerías", CUIT = 30222222220 };
            Proveedor prov3 = new Proveedor { Codigo = "E001", RazonSocial = "Energía y Conexiones Ltda.", CUIT = 30333333331 };
            Proveedor prov4 = new Proveedor { Codigo = "E002", RazonSocial = "Iluminación Innovadora S.R.L.", CUIT = 30444444442 };
            proveedores.Add(prov1);
            proveedores.Add(prov2);
            proveedores.Add(prov3);
            proveedores.Add(prov4);
            XDocument xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Proveedores")
                );
            foreach(Proveedor prove in proveedores)
            {
                XElement provXML = new XElement("Proveedor",
                    new XElement("Codigo", prove.Codigo),
                    new XElement("RazonSocial", prove.RazonSocial),
                    new XElement("CUIT", prove.CUIT.ToString()));
                xml.Element("Proveedores").Add(provXML);
            }
            xml.Save(_archivoProv);
            return true;

        }
    }
}
