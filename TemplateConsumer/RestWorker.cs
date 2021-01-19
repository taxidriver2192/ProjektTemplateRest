using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TemplateLib.Models;

namespace TemplateConsumer
{
    internal class RestWorker
    {
        // private const string Url = "http://localhost:54395/api/items/";
        private const string Url = "http://localhost:1337/api/items/";

        public RestWorker()
        {
        }

        public async void Start()
        {
            // Starting test on Item
            PrintHeader("Get all items");
            var allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
            PrintHeader("Get Item Id 18");
            try
            {
                var testItem1 = await GetOneItem(18);
                Console.WriteLine("item= " + testItem1);
            }
            catch (KeyNotFoundException notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }

            PrintHeader("Get Item id 2");
            try
            {
                var testItem2 = await GetOneItem(2);
                Console.WriteLine("item= " + testItem2);
            }
            catch (KeyNotFoundException notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }

            // Create Item Id 1337
            PrintHeader("Create Item Id 1337");
            var nyItemTemplate = new Item(1337, "Just one item", false, 55);
            await CreateNewItem(nyItemTemplate);

            // Update Item Id 1337
            allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
            PrintHeader("Update Item Id 1337");
            nyItemTemplate.Name = "NewItemRandomName";
            await UpdateItem(nyItemTemplate);

            // Deleting Item Id 1337
            allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
            PrintHeader("Delete Item Id 1337");
            await DeletingItem(1337);

            // Printing all Items
            allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
        }


        private void PrintHeader(string txt)
        {
            Console.WriteLine("=========================");
            Console.WriteLine(txt);
            Console.WriteLine("=========================");
        }

        public async Task<List<Item>> GetAllItems()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(Url);
                var items = JsonConvert.DeserializeObject<List<Item>>(json);
                return items;
            }
        }

        private async Task<Item> GetOneItem(int id)
        {
            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync(Url + id);

                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    var itemTemplate = JsonConvert.DeserializeObject<Item>(json);
                    return itemTemplate;
                }

                throw new KeyNotFoundException(
                    $"Error code={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }
        }

        private async Task CreateNewItem(Item item)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(item),
                    Encoding.UTF8,
                    "application/json");

                var resp = await client.PostAsync(Url, content);
                if (resp.IsSuccessStatusCode) return;

                throw new ArgumentException("Error on creating a new Item.");
            }
        }


        private async Task UpdateItem(Item item)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(item),
                    Encoding.UTF8,
                    "application/json");

                var resp = await client.PutAsync(Url + item.Id, content);
                if (resp.IsSuccessStatusCode) return;

                throw new ArgumentException("Error on updating a item.");
            }
        }


        private async Task DeletingItem(int nr)
        {
            using (var client = new HttpClient())
            {
                var resp = await client.DeleteAsync(Url + nr);
                if (resp.IsSuccessStatusCode) return;

                throw new ArgumentException("Error on deleting a item");
            }
        }
    }
}