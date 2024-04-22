using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public class VerificarProductoPint : Verificador
    {
        public bool CheckForm(TextBox codigo, TextBox descripcion, TextBox marca, TextBox precioUnitario, TextBox cantidad, TextBox color)
        {
            if (!CampoVacio(codigo))
            {
                if (EsNumerico(codigo))
                {
                    if (!CampoVacio(descripcion))
                    {
                        if (!CampoVacio(marca))
                        {
                            if (!CampoVacio(precioUnitario))
                            {
                                if (EsDecimal(precioUnitario))
                                {
                                    if (!CampoVacio(cantidad))
                                    {
                                        if (EsNumerico(cantidad))
                                        {
                                            if (!CampoVacio(color))
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Debe completar el campo de cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("El formato del campo cantidad es numerico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Debe completar el campo de cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("El formato del campo precio unitario es decimal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe completar el campo de Precio unitario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe completar el campo de Marca", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar el campo de Descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("El formato del campo codigo es numerico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe completar el campo de codigo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
