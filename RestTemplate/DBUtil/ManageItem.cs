using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TemplateLib.Models;

namespace RestTemplate.DBUtil
{
    public class ManageItem
    {
        private const string ConnectionString =
            @"Data Source=Server=tcp:server-eksamesemester.database.windows.net,1433;Initial Catalog=db-eksamesemester;Persist Security Info=False;User ID=tina9647;Password=Zeaws2EJ!2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private const string GetAllItem = "select * from Item";
        public IEnumerable<Item> Get()
        {
            var items = new List<Item>();
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(GetAllItem, conn))
            {
                conn.Open();
                Console.WriteLine(conn);
                var reader = cmd.ExecuteReader();
                Console.WriteLine(reader);
                while (reader.Read())
                {
                    var item = ReadNextItem(reader);
                    items.Add(item);
                    Console.WriteLine(item);
                }
                reader.Close();
            }
            Console.WriteLine(items);
            return items;
        }
        private const string GetOneItem = "select * from Item where id = @id";
        public Item GetOneById(int id)
        {
            Item item = new Item();
            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            using SqlCommand cmd = new SqlCommand(GetOneItem, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                item = ReadNextItem(reader);
            }

            return item;
        }
        private const string SqlAddItem = "insert into Item (Id, Name, Sold, Price) values (@Id, @Name, @Sold, @Price)";
        public bool AddItem(Item item)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqlCommand(SqlAddItem, conn);
            cmd.Parameters.AddWithValue("@Id", item.Id);
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
        private const string SqlUpdateItem = "UPDATE Item SET Id = @Id, Name = @Name, Sold = @Sold, Price = @Price WHERE Id = @Id";
        public bool UpdateItem(int id, Item item)
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

        private const string SqlDeleteItem = "DELETE FROM Item WHERE Id = @Id";
        public Item DeleteUser(int id)
        {
            Item item = GetOneById(id);
            if (item.Id != -1)
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
                Sold = reader.GetBoolean(2),
                Price = reader.GetInt32(3)
            };
            return item;
        }
    }
}