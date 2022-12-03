using Capa_Entidad;

namespace CoreCinCout.helpers
{
    public interface IConnection
    {
        Response GetConnection(UsuarioAdmin usuarioAdmin, string code);
    }
}
