using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace computershop.Services
{
    internal class Users : IDatabase
    {
        Connect conn = new Connect();

        public object AddRecord(string username, string fullname, string email, string password)
        {
            string salt = GenerateSalt();
            string hashedPassword = ComputeHmacSha256(password, salt);
            conn.Connection.Open();

            string sql = "INSERT INTO `users`(`UserName`, `FullName`, `Password`, `Email`, `Salt`) VALUES (@username,@fullname,@password,@email,@salt)";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@fullname", fullname);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", hashedPassword);
            cmd.Parameters.AddWithValue("@salt", salt);

            cmd.ExecuteNonQuery();
            conn.Connection.Close();

            return new { message = "Sikeres regisztráció!" };
        }


        public DataView GetAllData()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM users";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn.Connection);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            conn.Connection.Close();

            return dt.DefaultView;
        }

        public bool GetData(string username, string password)
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM users WHERE UserName = @username";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@username", username);

            MySqlDataReader reader = cmd.ExecuteReader();
            bool result = false;

            if (reader.Read())
            {
                string storedHash = reader.GetString(3);
                string storedSalt = reader.GetString(6);
                string compteHash = ComputeHmacSha256(password, storedSalt);
                result = true;
                conn.Connection.Close();

            }
            conn.Connection.Close();
            return result;

        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string ComputeHmacSha256(string password, string salt)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}