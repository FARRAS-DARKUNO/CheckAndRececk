
using bukitasam.DTOs;
using bukitasam.models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace bukitasam.Repositories
{
    public class HistoryRepository
    {
        private readonly string _connStr;
        public HistoryRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public bool InsertHistory(HistoryDTO data)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try
            {
                conn.Open();
                string sql = "INSERT INTO jual VALUES (DEFAULT, @Date, @Jumlah, @IdBarang, @idJenis)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Date", data.Date);
                cmd.Parameters.AddWithValue("@Jumlah", data.Jumlah);
                cmd.Parameters.AddWithValue("@IdBarang", data.IdBarang);
                cmd.Parameters.AddWithValue("@idJenis", data.IdJenis);

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

        public bool DeleteHistory(int id)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try
            {
                conn.Open();
                string sql = "DELETE FROM jual  WHERE jual.id_jual = @Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

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
        public List<HistoryModel> GetHistory(string filter, string? seacrh)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);
            List<HistoryModel> result = new List<HistoryModel>();

            try
            {
                conn.Open();
                string sql = "";

                seacrh ??= "";

                Console.WriteLine(seacrh);

                if (filter == "waktu-lama")
                {
                    sql = "SELECT jual.id_jual, barang.name, jual.jumlah, jenis.name, jual.date FROM jual JOIN barang ON barang.id_barang = jual.fk_id_barang JOIN jenis ON jenis.id_jenis = jual.fk_id_jenis WHERE barang.name LIKE CONCAT('%', @Search, '%') ORDER BY jual.date ASC";
                }
                else if (filter == "waktu-baru")
                {
                    sql = "SELECT jual.id_jual, barang.name, jual.jumlah, jenis.name, jual.date FROM jual JOIN barang ON barang.id_barang = jual.fk_id_barang JOIN jenis ON jenis.id_jenis = jual.fk_id_jenis WHERE barang.name LIKE CONCAT('%', @Search, '%') ORDER BY jual.date DESC";
                }
                else if (filter == "nama-asc")
                {
                    sql = "SELECT jual.id_jual, barang.name, jual.jumlah, jenis.name, jual.date FROM jual JOIN barang ON barang.id_barang = jual.fk_id_barang JOIN jenis ON jenis.id_jenis = jual.fk_id_jenis WHERE barang.name LIKE CONCAT('%', @Search, '%') ORDER BY barang.name ASC";
                }
                else if (filter == "nama-desc")
                {
                    sql = "SELECT jual.id_jual, barang.name, jual.jumlah, jenis.name, jual.date FROM jual JOIN barang ON barang.id_barang = jual.fk_id_barang JOIN jenis ON jenis.id_jenis = jual.fk_id_jenis WHERE barang.name LIKE CONCAT('%', @Search, '%') ORDER BY barang.name DESC";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Search", seacrh);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new HistoryModel
                    {
                        Id = reader.GetInt32(0),
                        Nama = reader.GetString(1),
                        Jumlah = reader.GetInt32(2),
                        Jenis = reader.GetString(3),
                        Tanggal = DateTime.Parse(reader.GetString(4))
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return result;
        }

    }
}