using Capa_Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class SDB_Usuario : IDB_Usuario
    {
        public async Task<bool> BD_Search_Email(string email, string dni, string conn)
        {
            SqlConnection cn = new();

            bool respuesta = false;

            try
            {
                SqlCommand cmd = new();
                cn.ConnectionString = conn;
                cmd.CommandText = "P_Count_Email_Usuario";
                cmd.Connection = cn;
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@dni", dni);

                await cn.OpenAsync();

                int getValue = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                if (getValue > 0)
                {
                    respuesta = true;
                }
                else
                {
                    respuesta = false;
                }

                cmd.Parameters.Clear();
                cmd.Dispose();
                cn.Close();

                return respuesta;
            }
            catch (Exception)
            {

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                return respuesta;
            }
        }

        public async Task<DataTable> DB_Login(string email, string conn)
        {
            SqlConnection cn = new();
            DataTable data = new();

            try
            {
                cn.ConnectionString = conn;
                SqlDataAdapter da = new("P_Usuario_Login", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", email);

                await cn.OpenAsync();

                da.Fill(data);
                cn.Close();
                return data;

            }
            catch (Exception)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                return data;
            }
        }

        public async Task<int> DB_Register_Usuario(Usuario request, string conn)
        {
            SqlConnection cn = new();

            int respuesta;

            try
            {
                SqlCommand cmd = new();
                cn.ConnectionString = conn;
                cmd.CommandText = "P_Regiter_Usuario";
                cmd.Connection = cn;
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", request.FullName.Trim());
                cmd.Parameters.AddWithValue("@IdRol", request.IdRol);
                cmd.Parameters.AddWithValue("@Email", request.Email.Trim());
                cmd.Parameters.AddWithValue("@Password", request.Password);
                cmd.Parameters.AddWithValue("@CellPhone", request.CellPhone.Trim());
                cmd.Parameters.AddWithValue("@DNI", request.DNI.Trim());

                await cn.OpenAsync();

                int getValue = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                if (getValue > 0)
                {
                    respuesta = getValue;
                }
                else
                {
                    respuesta = 0;
                }

                cmd.Parameters.Clear();
                cmd.Dispose();
                cn.Close();
            }
            catch (Exception)
            {

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                return 0;
            }

            return respuesta;
        }
    }
}
