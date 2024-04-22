using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public class VerificarContratado : Verificador
    {
        public bool CheckForm(TextBox cantidad)
        {
            if (!CampoVacio(cantidad))
            {
                if (EsNumerico(cantidad))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("El formato de campo Cantidad no es correrto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe completar el campo de Cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
