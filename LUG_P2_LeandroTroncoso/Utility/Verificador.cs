using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public abstract class Verificador
    {
        private Regex _regex;
        protected bool CampoVacio(TextBox txt)
        {
            _regex = new Regex(@"^\s*$");
            if (!_regex.IsMatch(txt.Text)) return false;
            return true;
        }
        protected bool FormatoCorreo(TextBox txt)
        {
            _regex = new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            if (!_regex.IsMatch(txt.Text)) return false;
            return true;
        }
        protected bool EsDecimal(TextBox txt)
        {
            _regex = new Regex(@"^-?\d+([.,]\d{1,2})?$");
            if (!_regex.IsMatch(txt.Text)) return false;
            return true;
        }
        protected bool EsNumerico(TextBox txt)
        {
            _regex = new Regex(@"^\d+$");
            if (!_regex.IsMatch(txt.Text)) return false;
            return true;
        }
    }
}
