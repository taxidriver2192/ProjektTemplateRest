using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TemplateLib.Models;

namespace RestTemplate.DBUtil
{
    public class ManageItem
    {
        private const string ConnectionString =
            @"Server=tcp:server-eksamesemester.database.windows.net,1433;Initial Catalog=db-eksamesemester;Persist Security Info=False;User ID=tina9647;Password=Zeaws2EJ!2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private const string GetAllItem = "select * from Food";
        public IEnumerable<Food> Get()
        {
            var items = new List<Food>();
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
        private const string GetOneItem = "select * from Food where Id = @id";
        public Food GetOneById(int id)
        {
            Food food = new Food();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GetOneItem, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        food = ReadNextItem(reader);
                    }

                }

            }
            return food;
        }

        private const string SqlAddItem = "insert into Food (Name, InStock, LowLevel) values (@Name, @InStock, @LowLevel)";
        public bool AddItem(Food food)
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqlCommand(SqlAddItem, conn);
            cmd.Parameters.AddWithValue("@Name", food.Name);
            cmd.Parameters.AddWithValue("@InStock", food.InStock);
            cmd.Parameters.AddWithValue("@LowLevel", food.LowLevel);
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
        private const string SqlUpdateItem = "UPDATE Food SET Name = @Name, InStock = @InStock, LowLevel = @LowLevel WHERE Id = @Id";
        public bool UpdateItem(int id, Food food)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            bool OK = true;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(SqlUpdateItem, conn))
            {
                conn.Open();
                {
                    cmd.Parameters.AddWithValue("@Id", food.Id);
                    cmd.Parameters.AddWithValue("@Name", food.Name);
                    cmd.Parameters.AddWithValue("@InStock", food.InStock);
                    cmd.Parameters.AddWithValue("@LowLevel", food.LowLevel);
                    
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

        private const string SqlDeleteItem = "DELETE FROM Food WHERE Id = @Id";
        public Food DeleteUser(int id)
        {
            Food food = GetOneById(id);
            if (food.Id != -1)
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
            return food;
        }
        private Food ReadNextItem(SqlDataReader reader)
        {
            var item = new Food
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                InStock = reader.GetBoolean(2),
                LowLevel = reader.GetInt32(3)
            };
            return item;
        }
    }
}