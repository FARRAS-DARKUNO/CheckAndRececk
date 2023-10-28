
using bukitasam.DTOs;
using bukitasam.models;
using MySql.Data.MySqlClient;

namespace bukitasam.Repositories
{
    public class JenisRepository
    {
        private readonly string _connStr;
        public JenisRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public bool InsertJenis(JenisDTO data)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try
            {
                conn.Open();
                string sql = "INSERT INTO jenis VALUE (DEFAULT, @Name)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", data.Name);

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

        public bool UpdateJenis(JenisDTO data, int id)
        {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try
            {
                conn.Open();
                string sql = "UPDATE jenis SET name=@Name WHERE jenis.id_jenis = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", data.Name);
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

        public List<JenisModel> GetJenis(string? serach)
        {

            MySqlConnection conn = new MySqlConnection(_connStr);
            List<JenisModel> result = new List<JenisModel>();
            try
            {
                conn.Open();
                string sql = "";
                if (serach == null || serach == "")
                {
                    sql = "SELECT * FROM jenis";

                }
                else
                {
                    sql = "SELECT * FROM jenis WHERE jenis.name LIKE CONCAT('%', @Search, '%')";
                }
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Search", serach);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new JenisModel
                    {
                        Id = reader.GetInt32(0),
                        Nama = reader.GetString(1),
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
    }
}