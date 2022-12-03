using System.Text.RegularExpressions;

namespace Capa_Validacion
{
    public class SValidarCampos : IValidarCampos
    {
        public bool ValidarEmail(string correo)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.])*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(correo, expresion))
            {
                if (Regex.Replace(correo, expresion, string.Empty).Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool ValidarLetrasNumeros(string value)
        {
            Regex regex = new("^[a-zA-Z0-9 ]+$");
            Match match = regex.Match(value);
            return match.Success;
        }

        public bool ValidarLonguitud(string value, int length1, int length2)
        {
            if (value.Length < length1 || value.Length > length2) return true;
            if (string.IsNullOrEmpty(value)) return true;

            Regex regex = new(@" ");
            Match match = regex.Match(value);
            if(match.Success) return true;

            return false;
        }

        public bool ValidarPassowrd(string password)
        {
            Regex regex = new(@"((?=.*\d)(?=.*[A-Z]).{8,12})");
            Match match = regex.Match(password);
            return match.Success;
        }

        public bool ValidarSoloLetras(string value)
        {
            Regex regex = new(@"^[a-zA-Z ]+$");
            Match match = regex.Match(value);
            return match.Success;
        }

    }
}
