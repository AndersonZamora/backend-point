using Capa_Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class SCategoriaDB : ICategoriaDB
    {
        public async Task<int> AddCategoria(ECategoriaR categoria, string cnn)
        {
            SqlConnection cn = new();
            int respuesta;
            try
            {
                SqlCommand cmd = new();
                cn.ConnectionString = cnn;
                cmd.CommandText = "Sp_addCategoria";
                cmd.Connection = cn;
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@categoria", categoria.Categoria.Trim());

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

        public async Task<bool> DeleteCategoria(int Id_Cat, string cnn)
        {
            bool respuesta = true;
            SqlConnection cn = new();
            try
            {

                cn.ConnectionString = cnn;

                SqlCommand cmd = new("Sp_deleteCategoria", cn)
                {
                    CommandTimeout = 20,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idCat", Id_Cat);

                await cn.OpenAsync();

                int getValue = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());

                if (getValue > 0)
                {
                    respuesta = false;
                }
                else
                {
                    respuesta = true;
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

        public async Task<bool> EditCategoria(ECategoria categoria, string cnn)
        {
            bool respuesta = false;
            SqlConnection cn = new();
            try
            {

                cn.ConnectionString = cnn;
                SqlCommand cmd = new("Sp_editCategoria", cn)
                {
                    CommandTimeout = 20,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idCat", categoria.Id_Cat);
                cmd.Parameters.AddWithValue("@catategoria", categoria.Categoria.Trim());

                await cn.OpenAsync();

                int getValue = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());

                if (getValue > 0)
                {
                    respuesta = false;
                }
                else
                {
                    respuesta = true;
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

        public async Task<DataTable> ListAllCategoria(string cnn)
        {
            DataTable data = new();
            SqlConnection cn = new();
            try
            {
                cn.ConnectionString = cnn;
                SqlDataAdapter da = new("Sp_editCategoria", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                da.Fill(data);
                da.Dispose();
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
    }
}
