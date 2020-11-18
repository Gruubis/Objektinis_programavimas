using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace GUI.Data
{
    class UsersRepository
    {
        public static User LoggedInUser;
        public int id;
        private SqlConnection conn;

        public UsersRepository()
        {
            conn = new SqlConnection(@"Server=.;Database=Shop_db;Integrated Security=true;");
        }
        public void Register(User user, string username) 
        {
            if (user.GetUserName() == username)
                throw new Exception("Use with this username already exists!");

            string sql = "insert into users(name, lastname, birthdate, username, password, isAdmin) " +
                "values (@name, @lastname, @birthdate, @username, @password, @isAdmin) ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", user.GetName());
            cmd.Parameters.AddWithValue("@lastname", user.GetLastName());
            cmd.Parameters.AddWithValue("@birthdate", user.GetBirthDate());
            cmd.Parameters.AddWithValue("@username", user.GetUserName());
            cmd.Parameters.AddWithValue("@password", user.GetPassword());
            cmd.Parameters.AddWithValue("@isAdmin", user.GetAdmin());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


           
        }

        public User Login(string username, string password)
        {
            try
            {
                string sql = "select id, name, lastname, birthdate, username, password, isAdmin from users " +
                "where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["name"].ToString();
                        string lastname = reader["lastname"].ToString();
                        DateTime birthdate = DateTime.Parse(reader["birthdate"].ToString());
                        string usrname = reader["username"].ToString();
                        string pasword = reader["password"].ToString();
                        string admin = reader["isAdmin"].ToString();
                        
                        User user = new User(name, lastname, birthdate, usrname, pasword, admin);
                        user.SetUserId(id);
                        return user;
                    }
                }
                conn.Close();

            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                
            }
            throw new Exception("wrong username or password");
            }
        public List<User> GetUsers()
        {
            List<User> usersList = new List<User>();
            string sql = "select id, name, lastname, birthdate, username, password, isAdmin from users";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string lastname = reader["lastname"].ToString();
                    DateTime birthdate = DateTime.Parse(reader["birthdate"].ToString());
                    string usrname = reader["username"].ToString();
                    string pasword = reader["password"].ToString();
                    string isAdmin = reader["isAdmin"].ToString();
                    User user = new User(name, lastname, birthdate, usrname, pasword, isAdmin);
                    user.SetUserId(id);
                    usersList.Add(user);

                }
                conn.Close();
                return usersList;

            }
        }
        public void RemUser(string usr)
        {
            List<User> usersList = GetUsers();
            foreach(User user in usersList)
            {
                 if(user.GetUserName().Equals(usr) && user.GetAdmin() == "true")
                {
                    throw new Exception($"User {usr} is Admin. ");
                }
            }
                string sql = "Delete from users where username=@usr and isAdmin=@admin";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@usr", usr);
                cmd.Parameters.AddWithValue("@admin", "false");
                cmd.ExecuteNonQuery();
            conn.Close();

          
            
            
        }
        public void ChangePassword(string pasword, string newPassword)
        {
            if (pasword == LoggedInUser.GetPassword())
            {
                if (newPassword != "" && newPassword != LoggedInUser.GetPassword())
                {
                    string sql = "update users set password=@newPassword where username=@username";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@username", LoggedInUser.GetUserName());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                    throw new Exception("password cannot be empty or match previous one");
            }
            else
                throw new Exception("password didnt match");

        }
    }

}
