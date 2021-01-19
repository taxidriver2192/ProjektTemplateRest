using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TemplateLib.Models;

namespace RestTemplate.DBUtil
{
    public class ManageFood
    {
        private const string ConnectionString =
            @"Server=tcp:server-eksamesemester.database.windows.net,1433;Initial Catalog=db-eksamesemester;Persist Security Info=False;User ID=tina9647;Password=Zeaws2EJ!2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private const string GetAllItem = "select * from Item";
        public IEnumerable<Item> Get()
        {
            var items = new List<Item>();
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(GetAllItem, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = ReadNextItem(reader);
                    items.Add(item);
                }
                reader.Close();
            }
            return items;
        }
<<<<<<< HEAD:RestTemplate/DBUtil/ManageFood.cs
        private const string GetOneItem = "select * from Food where Id = @id";
        public Food GetFoodById(int id)
=======
        private const string GetOneItem = "select * from Item where Id = @id";
        public Item GetOneById(int id)
>>>>>>> parent of 6720b11... Update:RestTemplate/DBUtil/ManageItem.cs
        {
            Item item = new Item();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GetOneItem, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        item = ReadNextItem(reader);
                    }

                }

            }
            return item;
        }

<<<<<<< HEAD:RestTemplate/DBUtil/ManageFood.cs
        private const string SqlAddItem = "insert into Food (Name, InStock, LowLevel) values (@Name, @InStock, @LowLevel)";
        public bool AddFood(Food food)
=======
        private const string SqlAddItem = "insert into Item (Name, Sold, Price) values (@Name, @Sold, @Price)";
        public bool AddItem(Item item)
>>>>>>> parent of 6720b11... Update:RestTemplate/DBUtil/ManageItem.cs
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqlCommand(SqlAddItem, conn);
            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@Sold", item.Sold);
            cmd.Parameters.AddWithValue("@Price", item.Price);
            bool OK;
            try
            {
                var rows = cmd.ExecuteNonQuery();
                OK = rows == 1;
            }
            catch (Exception)
            {
                OK = false;
            }

            return OK;
        }
<<<<<<< HEAD:RestTemplate/DBUtil/ManageFood.cs
        private const string SqlUpdateItem = "UPDATE Food SET Name = @Name, InStock = @InStock, LowLevel = @LowLevel WHERE Id = @Id";
        public bool UpdateFood(int id, Food food)
=======
        private const string SqlUpdateItem = "UPDATE Item SET Name = @Name, Sold = @Sold, Price = @Price WHERE Id = @Id";
        public bool UpdateItem(int id, Item item)
>>>>>>> parent of 6720b11... Update:RestTemplate/DBUtil/ManageItem.cs
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            bool OK = true;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(SqlUpdateItem, conn))
            {
                conn.Open();
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Sold", item.Sold);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch
                    {
                        OK = false;
                    }
                }

            }
            return OK;
        }

<<<<<<< HEAD:RestTemplate/DBUtil/ManageFood.cs
        private const string SqlDeleteItem = "DELETE FROM Food WHERE Id = @Id";
        public Food DeleteFood(int id)
        {
            Food food = GetFoodById(id);
            if (food.Id != -1)
=======
        private const string SqlDeleteItem = "DELETE FROM Item WHERE Id = @Id";
        public Item DeleteUser(int id)
        {
            Item item = GetOneById(id);
            if (item.Id != -1)
>>>>>>> parent of 6720b11... Update:RestTemplate/DBUtil/ManageItem.cs
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(SqlDeleteItem, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            return item;
        }
        private Item ReadNextItem(SqlDataReader reader)
        {
            var item = new Item
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
<<<<<<< HEAD:RestTemplate/DBUtil/ManageFood.cs
                InStock = reader.GetInt32(2),
                LowLevel = reader.GetInt32(3)
=======
                Sold = reader.GetBoolean(2),
                Price = reader.GetInt32(3)
>>>>>>> parent of 6720b11... Update:RestTemplate/DBUtil/ManageItem.cs
            };
            return item;
        }
    }
}