using Capa_Entidad;
using System.Data;

namespace Capa_Datos
{
    public interface IBD_CinCout
    {
        Task<bool> BD_Search_Email(string email, string conn);
        Task<bool> BD_Search_Email_Usuario_Admin(string email, string conn);
        Task<DataTable> DB_Login(string email, string conn);
        Task<DataTable> DB_Login_Usuario_Admin(string email, string conn);
        Task<int> DB_Register_CinCout(CinCoutRegister request, string conn);
        Task<int> DB_Register_Usuario_Admin(UsuarioAdminRegister request, string conn);
    }
}
