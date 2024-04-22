using BE;
using BLL;
using Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Utility;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            _bllCliente = new BLLCliente();
            _verifCli = new VerificarCliente();
            _cliente = new Cliente();

            _bllProveedor = new BLLProveedor();
            _bllPintura = new BLLProdPintureria();
            _bllElectricidad = new BLLProdElectricidad();
            _verifProdElec = new VerificarProductoElect();
            _verifProdPint = new VerificarProductoPint();
            _bllContratado = new BLLProductoContratado();
            InitializeComponent();
        }
        #region instancias
        //clientes
        private BLLCliente _bllCliente;
        private VerificarCliente _verifCli;
        private Cliente _cliente;

        //Proveedor
        private BLLProveedor _bllProveedor;
        private Proveedor _proveedor;

        //Producto
        private BLLProdPintureria _bllPintura;
        private BLLProdElectricidad _bllElectricidad;
        private ProductoElectricidad _Pelectricidad;
        private ProductoPintureria _pPintura;
        private VerificarProductoElect _verifProdElec;
        private VerificarProductoPint _verifProdPint;
        private BLLProductoContratado _bllContratado;
        #endregion

        #region General
        private void Form1_Load(object sender, EventArgs e)
        {
            if (_bllProveedor.CrearProveedoresXML())
            {
                MessageBox.Show("Se han agregado los proveedores.");
            }
            //ComboBox de Informe
            cbxInforme.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxInforme.Items.Add("El producto más contratado, por tipo y el monto.");
            cbxInforme.Items.Add("El producto menos contratado, por tipo y monto");
            cbxInforme.Items.Add("El monto total recaudado por tipo de producto");

            //ComboBoxCategoria
            CargarCbCategoria();
            cbxCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            //ComboboxProducto
            cbxProducto.Items.Add("Pintura");
            cbxProducto.Items.Add("Electricidad");
            cbxProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxProducto.SelectedIndex = 0;
            //dgvClientes
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvClientes.MultiSelect = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dgvProveedor
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dgvProductos
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.MultiSelect = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dgvPContratados
            dgvPContratados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPContratados.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvPContratados.MultiSelect = false;
            dgvPContratados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //carga
            CargardgvClientes();
            CargardgvProveedor();
            CargardgvProductos(0);
            //formProductos
            cbProductoEstado.Checked = true;
            rbPintura.Checked = true;
            pnlProductoElectricidad.Visible = false;
            pnlProductoPintura.Visible = true;
        }
        //Cargar grillas
        private void CargardgvClientes()
        {
            dgvClientes.DataSource = null;
            List<Cliente> clientes = _bllCliente.LeerTodo();
            if (clientes != null) dgvClientes.DataSource = clientes;
        }
        private void CargardgvProveedor()
        {
            dgvProveedores.DataSource = null;
            List<Proveedor> proveedors = _bllProveedor.LeerTodos();
            if (proveedors != null) dgvProveedores.DataSource = proveedors;
        }
        private void CargardgvProductos(int value)
        {
            dgvProductos.DataSource = null;
            if (value == 0)
            {
                dgvProductos.DataSource = _bllPintura.LeerTodo();
                LimpiarTxtProduct();
            }
            else if (value == 1)
            {
                dgvProductos.DataSource = _bllElectricidad.LeerTodo();
                LimpiarTxtProduct();
            }
        }
        private void CargardgvContratados(Cliente cliente)
        {
            dgvPContratados.DataSource = null;
            dgvPContratados.DataSource = _bllContratado.LeerProductoContratado(cliente);
        }
        //Limpar TXT
        private void LimpiarTxtCliente()
        {
            txtClienteCodigo.Text = "";
            txtClienteNombre.Text = "";
            txtClienteApellido.Text = "";
            txtClienteDNI.Text = "";
            txtClienteEmail.Text = "";
        }
        private void LimpiarTxtProduct()
        {
            txtProductoCodigo.Text = "";
            txtProductoDescripcion.Text = "";
            txtProductoMarca.Text = "";
            txtProductoPrecio.Text = "";
            txtProductoCantidad.Text = "";
            cbProductoEstado.Checked = true;
            _proveedor = null;
            cbxCategoria.SelectedIndex = 0;
            _pPintura = null;
            _Pelectricidad = null;
        }
        private void rbPintura_CheckedChanged(object sender, EventArgs e)
        {

            pnlProductoElectricidad.Visible = false;
            pnlProductoPintura.Visible = true;
        }
        private void rbElectricidad_CheckedChanged(object sender, EventArgs e)
        {
            pnlProductoElectricidad.Visible = true;
            pnlProductoPintura.Visible = false;
        }
        //cargar ComboBox
        private void CargarCbCategoria()
        {
            cbxCategoria.DataSource = Enum.GetValues(typeof(CategoriaEnum));
        }
        #endregion
        #region Clientes
        private void btnClienteAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_verifCli.CheckForm(txtClienteCodigo, txtClienteNombre, txtClienteApellido, txtClienteDNI, txtClienteEmail))
                {
                    if (!_bllCliente.ExisteCodigo(int.Parse(txtClienteCodigo.Text)))
                    {
                        if (!_bllCliente.ExisteDNI(long.Parse(txtClienteDNI.Text)))
                        {
                            Cliente cliente = new Cliente();
                            cliente.Codigo = int.Parse(txtClienteCodigo.Text);
                            cliente.Nombre = txtClienteNombre.Text;
                            cliente.Apellido = txtClienteApellido.Text;
                            cliente.DNI = int.Parse(txtClienteDNI.Text);
                            cliente.Email = txtClienteEmail.Text;
                            if (_bllCliente.Agregar(cliente))
                            {
                                MessageBox.Show("Se agrego con exito.");
                                CargardgvClientes();
                                LimpiarTxtCliente();
                            }
                        }MessageBox.Show("Dni existente");
                    }MessageBox.Show("Codigo Existente.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _cliente = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
                txtClienteCodigo.Text = _cliente.Codigo.ToString();
                txtClienteNombre.Text = _cliente.Nombre;
                txtClienteApellido.Text = _cliente.Apellido;
                txtClienteDNI.Text = _cliente.DNI.ToString();
                txtClienteEmail.Text = _cliente.Email;
                if (_cliente != null)
                {
                    CargardgvContratados(_cliente);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnClienteModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cliente != null)
                {
                    if (_verifCli.CheckForm(txtClienteCodigo, txtClienteNombre, txtClienteApellido, txtClienteDNI, txtClienteEmail))
                    {
                        Cliente clienteModif = _cliente;
                        clienteModif.Nombre = txtClienteNombre.Text;
                        clienteModif.Apellido = txtClienteApellido.Text;
                        clienteModif.Email = txtClienteEmail.Text;
                        clienteModif.DNI = long.Parse(txtClienteDNI.Text);
                        if (!_bllCliente.ExisteCodigo(_cliente.Codigo, true, _cliente))
                        {
                            if (!_bllCliente.ExisteDNI(_cliente.Codigo, true, _cliente))
                            {
                                if (_bllCliente.Modificar(clienteModif))
                                {
                                    MessageBox.Show("Se ha modificado.");
                                    _cliente = null;
                                    CargardgvClientes();
                                    LimpiarTxtCliente();
                                }
                            }
                            else MessageBox.Show("Ese DNI ya existe");
                        }
                        else MessageBox.Show("Ese codigo ya existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnClienteBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cliente != null)
                {
                    DialogResult result = MessageBox.Show("Desea eliminar el cliente?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (_bllCliente.Borrar(_cliente))
                        {
                            MessageBox.Show("Se ha borrado con exito.");
                            CargardgvClientes();
                            _cliente = null;
                        }
                    }
                }
                else MessageBox.Show($"No se puede eliminar el cliente ya que tiene un vehiculo asignado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Proveedores
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _proveedor = (Proveedor)dgvProveedores.CurrentRow.DataBoundItem;
                lblProductoProveedor.Text = _proveedor.RazonSocial.ToUpper();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Productos
        private void cbxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProducto.SelectedIndex == 0) CargardgvProductos(0);
            else CargardgvProductos(1);
            _pPintura = null;
            _Pelectricidad = null;
            lblPContratarNombre.Text = "";
            lblPContratarStock.Text = "";
            LimpiarTxtProduct();
        }
        private void btnProductoAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_proveedor != null)
                {
                    if (rbPintura.Checked)
                    {
                        if (_verifProdPint.CheckForm(txtProductoCodigo, txtProductoDescripcion, txtProductoMarca, txtProductoPrecio, txtProductoCantidad, txtProductoColor))
                        {
                            ProductoPintureria p = new ProductoPintureria();
                            p.Codigo = txtProductoCodigo.Text;
                            p.Descripcion = txtProductoDescripcion.Text;
                            p.Marca = txtProductoMarca.Text;
                            p.PrecioUnitario = decimal.Parse(txtProductoPrecio.Text);
                            p.Cantidad = int.Parse(txtProductoCantidad.Text);
                            p.Proveedor = _proveedor;
                            p.Estado = cbProductoEstado.Checked;
                            p.Color = txtProductoColor.Text;
                            if (_bllPintura.Agregar(p))
                            {
                                MessageBox.Show("Se ha creado con exíto");
                                LimpiarTxtProduct();
                                CargardgvProductos(0);
                            }
                        }
                    }
                    if (rbElectricidad.Checked)
                    {
                        if (_verifProdElec.CheckForm(txtProductoCodigo, txtProductoDescripcion, txtProductoMarca, txtProductoPrecio, txtProductoCantidad))
                        {
                            ProductoElectricidad p = new ProductoElectricidad();
                            p.Codigo = txtProductoCodigo.Text;
                            p.Descripcion = txtProductoDescripcion.Text;
                            p.Marca = txtProductoMarca.Text;
                            p.PrecioUnitario = decimal.Parse(txtProductoPrecio.Text);
                            p.Cantidad = int.Parse(txtProductoCantidad.Text);
                            p.Proveedor = _proveedor;
                            p.Estado = cbProductoEstado.Checked;
                            p.Categoria = (CategoriaEnum)cbxCategoria.SelectedValue;
                            if (_bllElectricidad.Agregar(p))
                            {
                                MessageBox.Show("Se ha creado con exíto");
                                LimpiarTxtProduct();
                                CargardgvProductos(0);
                            }

                        }
                    }
                }
                else MessageBox.Show("Debe seleccionar un proveedor");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cbxProducto.SelectedIndex == 0)
            {
                _pPintura = (ProductoPintureria)dgvProductos.CurrentRow.DataBoundItem;
                txtProductoCodigo.Text = _pPintura.Codigo.Replace("P", "");
                txtProductoDescripcion.Text = _pPintura.Descripcion;
                txtProductoMarca.Text = _pPintura.Marca;
                txtProductoPrecio.Text = _pPintura.PrecioUnitario.ToString();
                txtProductoCantidad.Text = _pPintura.Cantidad.ToString();
                cbProductoEstado.Checked = _pPintura.Estado;
                _proveedor = _pPintura.Proveedor;
                txtProductoColor.Text = _pPintura.Color;
                lblPContratarNombre.Text = _pPintura.Descripcion;
                lblPContratarStock.Text = _pPintura.Cantidad.ToString();
            }
            else if (cbxProducto.SelectedIndex == 1)
            {
                _Pelectricidad = (ProductoElectricidad)dgvProductos.CurrentRow.DataBoundItem;
                txtProductoCodigo.Text = _Pelectricidad.Codigo.Replace("E", "");
                txtProductoDescripcion.Text = _Pelectricidad.Descripcion;
                txtProductoMarca.Text = _Pelectricidad.Marca;
                txtProductoPrecio.Text = _Pelectricidad.PrecioUnitario.ToString();
                txtProductoCantidad.Text = _Pelectricidad.Cantidad.ToString();
                cbProductoEstado.Checked = _Pelectricidad.Estado;
                _proveedor = _Pelectricidad.Proveedor;
                cbxCategoria.SelectedIndex = (int)_Pelectricidad.Categoria;
                lblPContratarNombre.Text = _Pelectricidad.Descripcion;
                lblPContratarStock.Text = _Pelectricidad.Cantidad.ToString();
            }
        }

        private void btnProductoModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pPintura != null || _Pelectricidad != null)
                {
                    if (_proveedor != null)
                    {
                        if (cbxProducto.SelectedIndex == 0)
                        {
                            if (_verifProdPint.CheckForm(txtProductoCodigo, txtProductoDescripcion, txtProductoMarca, txtProductoPrecio, txtProductoCantidad, txtProductoColor))
                            {
                                if (!_bllPintura.ExisteCodigo(txtProductoCodigo.Text, true, _pPintura))
                                {
                                    string codigo = _pPintura.Codigo;
                                    _pPintura.Codigo = txtProductoCodigo.Text;
                                    _pPintura.Descripcion = txtProductoDescripcion.Text;
                                    _pPintura.Marca = txtProductoMarca.Text;
                                    _pPintura.PrecioUnitario = decimal.Parse(txtProductoPrecio.Text);
                                    _pPintura.Cantidad = int.Parse(txtProductoCantidad.Text);
                                    _pPintura.Proveedor = _proveedor;
                                    _pPintura.Estado = cbProductoEstado.Checked;
                                    _pPintura.Color = txtProductoColor.Text;
                                    if (_bllPintura.Modificar(_pPintura, codigo))
                                    {
                                        MessageBox.Show("Se ha modificado con exíto.");
                                        LimpiarTxtProduct();
                                        CargardgvProductos(0);
                                    }
                                }
                                else MessageBox.Show("El codigo ya se encuentra en uso.");
                            }
                        }
                        else if (cbxProducto.SelectedIndex == 1)
                        {
                            if (_verifProdElec.CheckForm(txtProductoCodigo, txtProductoDescripcion, txtProductoMarca, txtProductoPrecio, txtProductoCantidad))
                            {
                                if (!_bllElectricidad.ExisteCodigo(txtProductoCodigo.Text, true, _Pelectricidad))
                                {
                                    string codigo = _Pelectricidad.Codigo;
                                    _Pelectricidad.Codigo = txtProductoCodigo.Text;
                                    _Pelectricidad.Descripcion = txtProductoDescripcion.Text;
                                    _Pelectricidad.Marca = txtProductoMarca.Text;
                                    _Pelectricidad.PrecioUnitario = decimal.Parse(txtProductoPrecio.Text);
                                    _Pelectricidad.Cantidad = int.Parse(txtProductoCantidad.Text);
                                    _Pelectricidad.Proveedor = _proveedor;
                                    _Pelectricidad.Estado = cbProductoEstado.Checked;
                                    _Pelectricidad.Categoria = (CategoriaEnum)cbxCategoria.SelectedValue;
                                    if (_bllElectricidad.Modificar(_Pelectricidad, codigo))
                                    {
                                        MessageBox.Show("Se ha modificado con exíto.");
                                        LimpiarTxtProduct();
                                        CargardgvProductos(1);
                                    }
                                }
                            }
                        }
                    }
                    else MessageBox.Show("Debe seleccionar un proveedor.");
                }
                else MessageBox.Show("Debe seleccionar un producto");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnProductoBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_pPintura != null || _Pelectricidad != null)
                {
                    DialogResult result = MessageBox.Show("Desea eliminar el archivo", "Advertencia", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (cbxProducto.SelectedIndex == 0)
                        {
                            if (_bllPintura.Borrar(_pPintura))
                            {
                                MessageBox.Show("Se ha borrado con exito.");
                                CargardgvProductos(0);
                                _pPintura = null;
                            }
                        }
                        else if (cbxProducto.SelectedIndex == 1)
                        {
                            if (_bllElectricidad.Borrar(_Pelectricidad))
                            {
                                MessageBox.Show("Se ha borrado con exito.");
                                CargardgvProductos(1);
                                _Pelectricidad = null;
                            }
                        }
                    }
                }
                else MessageBox.Show("Debe seleccionar un producto");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Producto Contratado
        private void btnProductoContratar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cliente != null)
                {


                    if (cbxProducto.SelectedIndex == 0)
                    {
                        if (_pPintura != null)
                        {

                            if (!_bllContratado.ProductoYaContratado(_cliente.Codigo,_pPintura.Codigo))
                            {
                                if (_bllContratado.VerificarStock(_pPintura.Cantidad, int.Parse(txtpContratadoCantidad.Text)))
                                {
                                    if (_bllContratado.ContratarProduct(int.Parse(txtpContratadoCantidad.Text), _cliente, _pPintura))
                                    {
                                        MessageBox.Show("Se contrato el producto con exito");
                                        _pPintura.Cantidad = _pPintura.Cantidad - int.Parse(txtpContratadoCantidad.Text);
                                        _bllElectricidad.Modificar(_pPintura, _pPintura.Codigo);
                                        LimpiarTxtProduct();
                                        LimpiarTxtCliente();
                                        txtpContratadoCantidad.Text = "";
                                        lblPContratarNombre.Text = "";
                                        lblPContratarStock.Text = "";
                                    }
                                }
                                else MessageBox.Show("El producto no tiene el stock suficiente");
                            }
                            else MessageBox.Show("El producto ya esta contratado");

                        }
                    }
                    else if (cbxProducto.SelectedIndex == 1)
                    {
                        if (_Pelectricidad != null)
                        {
                            if (!_bllContratado.ProductoYaContratado(_cliente.Codigo, _Pelectricidad.Codigo))
                            {
                                if (_bllContratado.VerificarStock(_Pelectricidad.Cantidad, int.Parse(txtpContratadoCantidad.Text)))
                                {
                                    if (_bllContratado.ContratarProduct(int.Parse(txtpContratadoCantidad.Text), _cliente, _Pelectricidad))
                                    {
                                        MessageBox.Show("Se contrato el producto con exito");
                                        _Pelectricidad.Cantidad = _Pelectricidad.Cantidad - int.Parse(txtpContratadoCantidad.Text);
                                        _bllElectricidad.Modificar(_Pelectricidad, _Pelectricidad.Codigo);
                                        LimpiarTxtProduct();
                                        LimpiarTxtCliente();
                                        txtpContratadoCantidad.Text = "";
                                        lblPContratarNombre.Text = "";
                                        lblPContratarStock.Text = "";
                                    }
                                }
                                else MessageBox.Show("Stock insuficiente");
                            }
                            else MessageBox.Show("El producto ya esta contratado");

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        private void dgvPContratados_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbxInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxInforme.SelectedIndex == 0)
            {
                Dictionary<string, decimal> informe = _bllContratado.InformeMasContratado();
                cInforme.Titles.Clear();
                cInforme.ChartAreas.Clear();
                cInforme.Series.Clear();
                Title titulo = new Title("Tipo de producto mas contratado");
                cInforme.Titles.Add(titulo);
                ChartArea area = new ChartArea();
                area.Area3DStyle.Enable3D = true;
                cInforme.ChartAreas.Add(area);
                Series serie = new Series("Por tipo");
                serie.ChartType = SeriesChartType.Column;
                serie.Points.DataBindXY(informe.Keys, informe.Values);
                cInforme.Series.Add(serie);
            }
            else if (cbxInforme.SelectedIndex == 1)
            {
                Dictionary<string, decimal> informe = _bllContratado.InformeMenosContratado();
                cInforme.Titles.Clear();
                cInforme.ChartAreas.Clear();
                cInforme.Series.Clear();
                Title titulo = new Title("Tipo de producto menos contratado");
                cInforme.Titles.Add(titulo);
                ChartArea area = new ChartArea();
                area.Area3DStyle.Enable3D = true;
                cInforme.ChartAreas.Add(area);
                Series serie = new Series("Por tipo");
                serie.ChartType = SeriesChartType.Column;
                serie.Points.DataBindXY(informe.Keys, informe.Values);
                cInforme.Series.Add(serie);
            }
            else
            {
                Dictionary<string, decimal> informe = _bllContratado.InformeTotal();
                cInforme.Titles.Clear();
                cInforme.ChartAreas.Clear();
                cInforme.Series.Clear();
                Title titulo = new Title("Monto total recaudado por tipo de producto");
                cInforme.Titles.Add(titulo);
                ChartArea area = new ChartArea();
                area.Area3DStyle.Enable3D = true;
                cInforme.ChartAreas.Add(area);
                Series serie = new Series("Por tipo");
                serie.ChartType = SeriesChartType.Column;
                serie.Points.DataBindXY(informe.Keys, informe.Values);
                cInforme.Series.Add(serie);
            }
        }
    }
}
