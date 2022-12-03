using Capa_Entidad;

namespace Capa_Validacion
{
    public class SValidarLoginRegister : IValidarLoginRegister
    {
        private readonly IValidarCampos mCampos;
        public SValidarLoginRegister(IValidarCampos mCampos)
        {
            this.mCampos = mCampos;
        }
         
        public Response ValidarLogin(Login request)
        {
            if (request == null) return new Response() { Msg = "Algunos de los campos no son correctos", Ok = false };
            if (mCampos.ValidarEmail(request.Email)) return new Response() { Msg = "Correo electrónico no válido", Ok = false };
            if (string.IsNullOrEmpty(request.Password)) return new Response() { Msg = "Algunos de los campos no son correctos", Ok = false };
            var passowrd1 = request.Password.Replace(" ", "");
            if (!mCampos.ValidarPassowrd(passowrd1)) return new Response() { Msg = "Credenciales incorrectas", Ok = false };

            return new Response() { Ok = true };
        }
        
        public Response ValidarRegisterCinCout(CinCoutRegister request, string code)
        {
            
            if (!code.Equals(request.State)) return new Response() { Ok = false, Msg = "Algunos de los campos no son correctos1" };
            if (mCampos.ValidarEmail(request.Email)) return new Response() { Msg = "Correo electrónico no válido", Ok = false };
            if(string.IsNullOrEmpty(request.Password)) return new Response() { Msg = "Algunos de los campos no son correctos", Ok = false };
            var passowrd1 = request.Password.Replace(" ", "");
            if (!mCampos.ValidarPassowrd(passowrd1)) return new Response() { Msg = "Algunos de los campos no son correctos6", Ok = false };
            if(passowrd1.Length < 8 || passowrd1.Length > 12 ) return new Response() { Msg = "Algunos de los campos no son correctos7", Ok = false };
            if (!mCampos.ValidarSoloLetras(request.FullName)) return new Response() { Msg = "Algunos de los campos no son correctos4", Ok = false };
            if (request.IdRol <= 0) return new Response() { Msg = "Algunos de los campos no son correctos5", Ok = false };
            return new Response() { Ok = true };
        }
        
        public Response ValidarRegisterUsuarioRegister(UsuarioAdminRegister request, string code)
        {
            var passowrd1 = request.Password.Replace(" ", "");
            var passowrd2 = request.Contrasenia.Replace(" ", "");

            if (!mCampos.ValidarSoloLetras(request.FullName)) return new Response() { Msg = "Algunos de los campos no son correctos", Ok = false };
            if (!mCampos.ValidarPassowrd(passowrd1)) return new Response() { Msg = "Algunos de los campos no son correctos", Ok = false };
            if (mCampos.ValidarEmail(request.Email)) return new Response() { Msg = "Algunos de los campos no son correctos", Ok = false };
            if (request.IdRol != 7) return new Response() { Msg = "Algunos de los campos no son correctos I", Ok = false };
            if (mCampos.ValidarLonguitud(request.Usuername, 5, 30)) return new Response() { Msg = "1 Algunos de los campos no son correctos", Ok = false };
            if (mCampos.ValidarLonguitud(request.Data, 5, 30)) return new Response() { Msg = "2 Algunos de los campos no son correctos", Ok = false };
            if (mCampos.ValidarLonguitud(request.Initial, 5, 30)) return new Response() { Msg = "3 Algunos de los campos no son correctos", Ok = false };
            if (mCampos.ValidarLonguitud(request.User, 5, 30)) return new Response() { Msg = "4 Algunos de los campos no son correctos", Ok = false };
            if (!mCampos.ValidarPassowrd(passowrd2)) return new Response() { Msg = "5 Algunos de los campos no son correctos", Ok = false };

            return new Response() { Ok = true };
        }
    }
}
