namespace Capa_Validacion
{
    public interface IValidarCampos
    {
        bool ValidarEmail(string correo);
        bool ValidarPassowrd(string password);
        bool ValidarSoloLetras(string value);
        bool ValidarLonguitud(string value, int length1, int length2);
        bool ValidarLetrasNumeros(string value);
    }
}
