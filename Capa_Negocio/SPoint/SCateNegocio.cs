using Capa_Datos;
using Capa_Entidad;
using System.Data;

namespace Capa_Negocio
{
    public class SCateNegocio : ICateNegocio
    {
        private readonly ICategoriaDB mCategoria;
        public SCateNegocio(ICategoriaDB mCategoria)
        {
            this.mCategoria = mCategoria;
        }

        public async Task<int> AddCategoriaN(ECategoriaR categoria, string cnn)
        {
            return await mCategoria.AddCategoria(categoria, cnn);
        }

        public async Task<bool> DeleteCategoriaN(int Id_Cat, string cnn)
        {
            return await mCategoria.DeleteCategoria(Id_Cat, cnn);
        }

        public async Task<bool> EditCategoriaN(ECategoria categoria, string cnn)
        {
            return await mCategoria.EditCategoria(categoria, cnn);
        }

        public async Task<List<ECategoria>> ListAllCategoriaN(string cnn)
        {
            DataTable data = await mCategoria.ListAllCategoria(cnn);
            var list = EvaluateData(data);
            return list;
        }

        private static List<ECategoria> EvaluateData(DataTable data)
        {
            List<ECategoria> list = new();

            try
            {
                if (data.Rows.Count > 0)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        var id = Convert.ToInt16(row["Id_Cat"].ToString());
                        if (id <= 0) break;

                        var categoria = row["Categoria"].ToString();
                        if (string.IsNullOrEmpty(categoria)) break;

                        ECategoria env = new()
                        {
                            Id_Cat = id,
                            Categoria = categoria,
                        };

                        list.Add(env);
                    }

                    return list;
                }

                return list;
            }
            catch (Exception)
            {
                return list;
            }
        }
    }
}
