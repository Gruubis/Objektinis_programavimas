using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Data;

namespace GUI.Data
{
    class UsersRepository
    {
        public static User LoggedInUser;
        private SqlConnection conn;
        public byte[] Content { get; set; }
        public UsersRepository()
        {
            conn = new SqlConnection(@"Server=.;Database=Shop_db;Integrated Security=true;");
        }
        public void Register(User user, string username)
        {

            var people = GetUsers();
            foreach (var User in people)
            {
                if (User.GetUserName().Equals(username))
                    throw new Exception("User already exists");
            }

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
                string sql = "select id, name, lastname, birthdate, username, password, isAdmin, image from users " +
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
                        string img = reader["image"].ToString();
                        User user = new User(name, lastname, birthdate, usrname, pasword, admin);
                        user.setImage(img);
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
        public void RemUser(int id, string usr)
        {
            List<User> usersList = GetUsers();
            foreach (User user in usersList)
            {
                if (user.GetUserName().Equals(usr) && user.GetAdmin() == "true")
                {
                    throw new Exception($"User {usr} is Admin. ");
                }
            }
            string sql = "Delete from users where id=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id", id);
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

        public void SavePicture(string img, int id)
        {

            SqlCommand cmd = new SqlCommand("update users set image=@image where id=@id", conn);
            cmd.Parameters.AddWithValue("@image", img);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        public List<Item> GetItems()
        {
            List<Item> ItemsList = new List<Item>();
            try
            {
                string sql = " select id, price, title, description, image from items";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string title = reader["title"].ToString();
                        double price = double.Parse(reader["price"].ToString());
                        string description = reader["description"].ToString();
                        string image = reader["image"].ToString();
                        ItemsList.Add(new Item(id, title, description, image, price));
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
            return ItemsList;
        }
        public List<Category> GetCategories()
        {
            List<Category> CategoryList = new List<Category>();
            try
            {
                string sql = "select id, title from category";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string title = reader["title"].ToString();
                        CategoryList.Add(new Category(id, title));
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
            foreach (Category c in CategoryList)
            {
                c.SetItems(GetItems(c.Id));
            }
            return CategoryList;
        }
        private List<Item> GetItems(int categoryid)
        {
            List<Item> ItemsList = new List<Item>();
            try
            {
                string sql = "select id, price, title, description, image from items " +
                    "where categoryid=@categoryid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@categoryid", categoryid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string title = reader["title"].ToString();
                    double price = double.Parse(reader["price"].ToString());
                    string description = reader["description"].ToString();
                    string image = reader["image"].ToString();
                    ItemsList.Add(new Item(id, title, description, image, price));
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
            return ItemsList;
        }

        public void AddToWhishlist(int userId, int itemId)
        {
            string sql = "insert into wishlist (userid, itemid) values (@userid, @itemid)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userId);
            cmd.Parameters.AddWithValue("@itemid", itemId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public List<Item> GetWishlist(int userid)
        {
            List<Item> WishList = new List<Item>();
            string sql = "select * from items where id IN (select itemid from wishlist where userid=@userid)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = int.Parse(reader["id"].ToString());
                string title = reader["title"].ToString();
                double price = double.Parse(reader["price"].ToString());
                string description = reader["description"].ToString();
                string image = reader["image"].ToString();
                WishList.Add(new Item(id, title, description, image, price));
            }
            conn.Close();
            return WishList;
        }

        public void DeleteLiked(int id)
        {
            string sql = "delete from wishlist where itemid=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void AddComment(int itemId, string comment, int userId)
        {
            DateTime date = DateTime.Now;
            string sql = "insert into comments (text, itemid, userid, date) values(@text, @itemid, @userid, @date)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@text", comment);
            cmd.Parameters.AddWithValue("@itemid", itemId);
            cmd.Parameters.AddWithValue("@userid", userId);
            cmd.Parameters.AddWithValue("@date", date.ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteComment(int userid, int itemid, string text)
        {
            string sql = "delete from comments where userid=@userid and itemid=@itemid and text=@text";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@itemid", itemid);
            cmd.Parameters.AddWithValue("@text", text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteItem(int itemId)
        {
            string sql = "delete from items where id=@itemid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@itemid", itemId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void AddNewCategory(string title)
        {
            string sql = "insert into category (title) values(@title)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@title", title);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void addNewItem(double price, string title, int categoryid, string description, string image)
        {
            string sql = "insert into items (price, title, categoryid, description, image) values(@price, @title, @categoryid, @description, @image)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@categoryid", categoryid);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@image", image);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public List<Comment> GetCommentsList()
        {
            List<Comment> comments = new List<Comment>();
            string sql = "select id, itemid,date, text from comments join users on users.id = comments.userid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string date = reader["date"].ToString();
                string text = reader["text"].ToString();
                int id = int.Parse(reader["id"].ToString());
                int itemid = int.Parse(reader["itemid"].ToString());
                comments.Add(new Comment(id, text, date, itemid));
            }
            conn.Close();
            return comments;
        }
    }
}
