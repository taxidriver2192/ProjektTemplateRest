using System.Collections.Generic;
using System.Data.SqlClient;
using TemplateLib.Models;

namespace RestTemplate.DBUtil
{
    public class ManageItem
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ClassDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GetAll = "select * from Item";
        public IEnumerable<Item> Get()
        {
            List<Item> list;
            list = new List<Item>();
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(GetAll, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = ReadNextElement(reader);
                    list.Add(item);
                }

                reader.Close();
            }
            return list;
        }

        private Item ReadNextElement(SqlDataReader reader)
        {
            var item = new Item();
            item.Id = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            item.Sold = reader.GetBoolean(2);
            item.Price = reader.GetInt32(3);
            return item;
        }
    }
}