using Capa_Entidad;
using Capa_Negocio;

namespace CoreCinCout.helpers
{
    public class SConnection : IConnection
    {
        private readonly ICreateHash mHash;
        public SConnection(ICreateHash mHash)
        {
            this.mHash = mHash;
        }

        public Response GetConnection(UsuarioAdmin usuarioAdmin, string code)
        {
            if (usuarioAdmin == null) return new() { Ok = false, Msg = "Something went wrong error code 526, contact CinCout" };
            
            if (string.IsNullOrEmpty(usuarioAdmin.Usuername)) return new() { Ok = false, Msg = "Something went wrong error code 526, contact CinCout" };
            if (string.IsNullOrEmpty(usuarioAdmin.Data)) return new() { Ok = false, Msg = "Something went wrong error code 526, contact CinCout" };
            if (string.IsNullOrEmpty(usuarioAdmin.Initial)) return new() { Ok = false, Msg = "Something went wrong error code 526, contact CinCout" };
            if (string.IsNullOrEmpty(usuarioAdmin.User)) return new() { Ok = false, Msg = "Something went wrong error code 526, contact CinCout" };
            if (string.IsNullOrEmpty(usuarioAdmin.Contrasenia)) return new() { Ok = false, Msg = "Something went wrong error code 526, contact CinCout" };

            string desUsername = mHash.PasswordDecrypt(usuarioAdmin.Usuername, code);
            string desData = mHash.PasswordDecrypt(usuarioAdmin.Data, code);
            string desInitial = mHash.PasswordDecrypt(usuarioAdmin.Initial, code);
            string desUser = mHash.PasswordDecrypt(usuarioAdmin.User, code);
            string desContrasenia = mHash.PasswordDecrypt(usuarioAdmin.Contrasenia, code);

            if (string.IsNullOrEmpty(desUsername)) return new() { Ok = false, Msg = "Something went wrong error code 1144, contact CinCout" };
            if (string.IsNullOrEmpty(desData)) return new() { Ok = false, Msg = "Something went wrong error code 1144, contact CinCout" };
            if (string.IsNullOrEmpty(desInitial)) return new() { Ok = false, Msg = "Something went wrong error code 1144, contact CinCout" };
            if (string.IsNullOrEmpty(desUser)) return new() { Ok = false, Msg = "Something went wrong error code 1144, contact CinCout" };
            if (string.IsNullOrEmpty(desContrasenia)) return new() { Ok = false, Msg = "Something went wrong error code 1144, contact CinCout" };

            //Server=;Database=PonintSale;Trusted_Connection=True;MultipleActiveResultSets=True
            string cnn = $"Server=;Database={desInitial};Trusted_Connection=True;MultipleActiveResultSets=True";

            return new() { Ok = true, Token = cnn };
        }
    }
}
