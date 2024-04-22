using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public class VerificarCliente : Verificador
    {
        public bool CheckForm(TextBox codigo,TextBox nombre,TextBox apellido,TextBox DNI,TextBox email)
        {
            if (!CampoVacio(codigo))
            {
                if (EsNumerico(codigo))
                {
                    if (!CampoVacio(nombre))
                    {
                        if (!CampoVacio(apellido))
                        {
                            if (!CampoVacio(DNI))
                            {
                                if (EsNumerico(DNI))
                                {
                                    if (!CampoVacio(email))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Debe completar el campo de email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("El formato del campo DNI es numerico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe completar el campo de DNI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe completar el campo de apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar el campo de nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
