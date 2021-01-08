using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Data
{
    class ItemsRepository
    {
        private SqlConnection conn;
    public ItemsRepository()
        {
            conn = new SqlConnection(@"Server=.;Database=Shop_db;Integrated Security=true;");
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

