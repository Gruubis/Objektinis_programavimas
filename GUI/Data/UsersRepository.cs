using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GUI.Data
{
    class UsersRepository
    {
        private static List<User> usersList;
        public static User LoggedInUser = null;

        private SqlConnection conn;

        public UsersRepository()
        {
            conn = new SqlConnection(@"Server=.;Database=Shop_db;Integrated Security=true;");
        }
        public void Register(User user, string username) 
        {
            try
            {
                
                string sql = "insert into users(name, lastname, birthdate, username, password) " +
                    "values (@name, @lastname, @birthdate, @username, @password)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", user.GetName());
                cmd.Parameters.AddWithValue("@lastname", user.GetLastName());
                cmd.Parameters.AddWithValue("@birthdate", user.GetBirthDate());
                cmd.Parameters.AddWithValue("@username", user.GetUserName());
                cmd.Parameters.AddWithValue("@password", user.GetPassword());
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        public User Login(string username, string password)
        {
            foreach (User user in usersList)
            {
                if (user.GetUserName().Equals(username) && user.GetPassword().Equals(password))
                    return user;
            }
            throw new Exception("Bad credentials");
        }
        public static List<User> GetUsers()
        {
            return usersList;

        }
        public void RemUser(string usr)
        {
            foreach (User user in usersList)
            {
                if (user.GetUserName().Equals(usr) && user.GetAdmin() == false)
                {
                    usersList.Remove(user);
                    return;
                }
            }
            throw new Exception($"User {usr} is Admin. ");
        }
    }

}
