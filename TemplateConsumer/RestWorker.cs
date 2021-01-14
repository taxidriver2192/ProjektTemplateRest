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
            // Starting test on Food
            PrintHeader("Get all items");
            var allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
            PrintHeader("Get Food Id 18");
            try
            {
                var testItem1 = await GetOneItem(18);
                Console.WriteLine("food= " + testItem1);
            }
            catch (KeyNotFoundException notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }

            PrintHeader("Get Food id 2");
            try
            {
                var testItem2 = await GetOneItem(2);
                Console.WriteLine("food= " + testItem2);
            }
            catch (KeyNotFoundException notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }

            // Create Food Id 1337
            PrintHeader("Create Food Id 1337");
            var nyItemTemplate = new Food(1337, "Just one food", false, 55);
            await CreateNewItem(nyItemTemplate);

            // Update Food Id 1337
            allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
            PrintHeader("Update Food Id 1337");
            nyItemTemplate.Name = "NewItemRandomName";
            await UpdateItem(nyItemTemplate);

            // Deleting Food Id 1337
            allItems = await GetAllItems();
            foreach (var item in allItems) Console.WriteLine(item);
            PrintHeader("Delete Food Id 1337");
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

        public async Task<List<Food>> GetAllItems()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(Url);
                var items = JsonConvert.DeserializeObject<List<Food>>(json);
                return items;
            }
        }

        private async Task<Food> GetOneItem(int id)
        {
            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync(Url + id);

                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    var itemTemplate = JsonConvert.DeserializeObject<Food>(json);
                    return itemTemplate;
                }

                throw new KeyNotFoundException(
                    $"Error code={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }
        }

        private async Task CreateNewItem(Food food)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(food),
                    Encoding.UTF8,
                    "application/json");

                var resp = await client.PostAsync(Url, content);
                if (resp.IsSuccessStatusCode) return;

                throw new ArgumentException("Error on creating a new Food.");
            }
        }


        private async Task UpdateItem(Food food)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(food),
                    Encoding.UTF8,
                    "application/json");

                var resp = await client.PutAsync(Url + food.Id, content);
                if (resp.IsSuccessStatusCode) return;

                throw new ArgumentException("Error on updating a food.");
            }
        }


        private async Task DeletingItem(int nr)
        {
            using (var client = new HttpClient())
            {
                var resp = await client.DeleteAsync(Url + nr);
                if (resp.IsSuccessStatusCode) return;

                throw new ArgumentException("Error on deleting a food");
            }
        }
    }
}