using Capa_Datos;
using Capa_Entidad;
using System.Data;

namespace Capa_Negocio
{
    public class SLoginCinCout : ILoginCinCout
    {
        private readonly IBD_CinCout mUsuario;

        public SLoginCinCout(IBD_CinCout mUsuario)
        {
            this.mUsuario = mUsuario;
        }

        public ClaimsIdent CompleteIdent(CinCoutRegister request, int id)
        {
            return new() { IdRol = request.IdRol, Names = request.FullName, Id = id, Email = request.Email };
        }

        public ClaimsIdent CompleteIdent(CinCout request)
        {
            return new() { IdRol = request.IdRol, Names = request.FullName, Id = request.Id, Email = request.Email };
        }

        public ClaimsIdent CompleteIdent(UsuarioAdmin request)
        {
            return new() { IdRol = request.IdRol, Names = request.FullName, Id = request.Id, Email = request.Email };
        }

        public async Task<CinCout> GetUser(string email, string conn)
        {
            try
            {
                DataTable data = await mUsuario.DB_Login(email, conn);

                if (data.Rows.Count > 0)
                {
                    CinCout usuario = new()
                    {
                        Id = Convert.ToInt16(data.Rows[0]["Id"]),
                        FullName = data.Rows[0]["FullName"].ToString(),
                        IdRol = Convert.ToInt16(data.Rows[0]["IdRol"]),
                        Password = data.Rows[0]["Password"].ToString(),
                        Email = data.Rows[0]["Email"].ToString()
                    };

                    return usuario;
                }

                return new();
            }
            catch (Exception)
            {
                return new();
            }
        }

        public async Task<UsuarioAdmin> GetUsuarioAdmin(string email, string conn)
        {
            try
            {
                DataTable data = await mUsuario.DB_Login_Usuario_Admin(email, conn);

                if (data.Rows.Count > 0)
                {
                    UsuarioAdmin usuario = new()
                    {
                        Id = Convert.ToInt16(data.Rows[0]["Id"]),
                        FullName = data.Rows[0]["FullName"].ToString(),
                        IdRol = Convert.ToInt16(data.Rows[0]["IdRol"]),
                        Password = data.Rows[0]["Password"].ToString(),
                        Email = data.Rows[0]["Email"].ToString()
                    };

                    return usuario;
                }

                return new();
            }
            catch (Exception)
            {
                return new();
            }
        }

        public async Task<int> PostCinCout(CinCoutRegister request, string conn)
        {
            return await mUsuario.DB_Register_CinCout(request, conn);
        }

        public async Task<int> PostUsuarioAdmin(UsuarioAdminRegister request, string conn)
        {
            return await mUsuario.DB_Register_Usuario_Admin(request, conn);
        }

        public async Task<bool> SearchEmail(string email, string conn)
        {
            return await mUsuario.BD_Search_Email(email, conn);
        }

        public async Task<bool> SearchEmailUsuarioAdmin(string email, string conn)
        {
            return await mUsuario.BD_Search_Email_Usuario_Admin(email, conn);
        }
    }
}
