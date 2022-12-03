using Capa_Entidad;
using System.Data;

namespace Capa_Datos
{
    public interface IDB_Usuario
    {
        Task<bool> BD_Search_Email(string email, string dni, string conn);
        Task<DataTable> DB_Login(string email, string conn);
        Task<int> DB_Register_Usuario(Usuario request, string conn);
    }
}
