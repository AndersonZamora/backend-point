using Capa_Entidad;
using System.Data.SqlClient;

namespace CoreCinCout.helpers
{
    public class SCheckConnection : ICheckConnection
    {
        public Response Connection(string cnn)
        {
            try
            {
                SqlConnection cn = new()
                {
                    ConnectionString = cnn
                };
                cn.Open();
                cn.Close();

                return new() { Ok = true, Msg = "Connection Open" };
            }
            catch (Exception)
            {
                return new() { Ok = false, Msg = "Connection Error" };
            }
        }
    }
}
