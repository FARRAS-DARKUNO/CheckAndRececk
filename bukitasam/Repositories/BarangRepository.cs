
using bukitasam.DTOs;
using bukitasam.models;
using MySql.Data.MySqlClient;

namespace bukitasam.Repositories
{
    public class BarangRepository
    {
        private readonly string _connStr;
        public BarangRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public bool InsertBarang(BarangDTO data)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try
            {
                conn.Open();
                string sql = "INSERT INTO barang VALUE (DEFAULT, @Name, @Stock)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@Stock", data.Stock);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
            conn.Close();

            return true;
        }

        public List<BarangModel> GetBarang(string? serach)
        {

            MySqlConnection conn = new MySqlConnection(_connStr);
            List<BarangModel> result = new List<BarangModel>();
            try
            {
                conn.Open();
                string sql = "SELECT barang.id_barang, barang.name, barang.stock, MIN(jual.jumlah), MAX(jual.jumlah) FROM barang join jual ON jual.fk_id_barang = barang.id_barang WHERE barang.name LIKE CONCAT('%', @Search, '%') GROUP BY barang.name";
                serach ??= "";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Search", serach);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new BarangModel
                    {
                        Id = reader.GetInt32(0),
                        Nama = reader.GetString(1),
                        Stock = reader.GetInt32(2),
                        Min = reader.GetInt32(3),
                        Max = reader.GetInt32(4) 
                    });
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            conn.Close();
            Console.WriteLine("Done.");

            return result;
        }

        public bool UpdateBarang(BarangDTO data, int id)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try
            {
                conn.Open();
                string sql = "UPDATE barang SET name=@Name, stock= @Stock WHERE barang.id_barang = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@Stock", data.Stock);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
            conn.Close();

            return true;
        }
    }
}