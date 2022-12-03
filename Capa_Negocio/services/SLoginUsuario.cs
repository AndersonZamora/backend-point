using Capa_Datos;
using Capa_Entidad;
using System.Data;


namespace Capa_Negocio
{
    public class SLoginUsuario : ILoginUsuario
    {
        private readonly IDB_Usuario mUsuario;

        public SLoginUsuario(IDB_Usuario mUsuario)
        {
            this.mUsuario = mUsuario;
        }

        public async Task<Usuario> GetUser(string email, string conn)
        {
            try
            {
                DataTable data = await mUsuario.DB_Login(email, conn);

                if (data.Rows.Count > 0)
                {
                    Usuario usuario = new()
                    {
                        Id = Convert.ToInt16(data.Rows[0]["Id"]),
                        FullName = data.Rows[0]["FullName"].ToString(),
                        IdRol = Convert.ToInt16(data.Rows[0]["IdRol"]),
                        Email = data.Rows[0]["Email"].ToString(),
                        Password = data.Rows[0]["Password"].ToString(),
                        CellPhone = data.Rows[0]["CellPhone"].ToString(),
                        DNI = data.Rows[0]["DNI"].ToString()
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

        public async Task<int> PostUser(Usuario request, string conn)
        {
            return await mUsuario.DB_Register_Usuario(request, conn);
        }

        public async Task<bool> SearchEmail(string email, string dni, string conn)
        {
            return await mUsuario.BD_Search_Email(email, dni, conn);
        }
    }
}
