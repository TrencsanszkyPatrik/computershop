using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
            conn.Connection.Open();

            string sql = "INSERT INTO `users`( `UserName`, `FullName`, `Password`, `Email`) VALUES (@username, @fullname, @password, @email)";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@fullname", fullname);
            cmd.Parameters.AddWithValue("@email", email);

            cmd.ExecuteNonQuery();


            conn.Connection.Close();

            return new { message = "sikeres" };
        }

        public ICollection<object> GetAllData()
        {
            ICollection<object> data = new List<object>();
            conn.Connection.Open();



            conn.Connection.Close();

            return data;
        }

        public object GetData(string username, string password)
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM users WHERE UserName = @username AND Password = @password";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);


            string result = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = "Reagisztrált tag";
            } else result = "Nincs ilyen felhasználó";

            conn.Connection.Close();

            return new { message = result}
               
            ;
        }
    }
}
