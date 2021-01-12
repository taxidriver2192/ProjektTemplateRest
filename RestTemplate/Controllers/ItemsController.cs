using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TemplateLib.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        // data
        private static readonly List<Item> Data = new List<Item>
        {
            new Item(1, "iPhoen 4", false, 345),
            new Item(2, "iPhoen 4S", true, 400),
            new Item(3, "iPhoen 5", false, 550),
            new Item(4, "iPhoen 5S", false, 600),
            new Item(5, "iPhoen SE", true, 850),
            new Item(6, "iPhoen 6", false, 1100),
            new Item(7, "iPhoen 6S", true, 1350),
            new Item(8, "iPhoen 6S+", false, 1500)
        };

        // GET: api/<ItemsController>
        [HttpGet]
        [EnableCors("CorsApi")]
        public IEnumerable<Item> Get()
        {
            return Data;
        }
        // GET api/<ItemsController>/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            if (Data.Exists(p => p.Id == id)) return Ok(Data.Find(p => p.Id == id));
            return NotFound($"Item which id {id} dose not exist");
        }


        // GET: api/<ItemsController>
        [HttpGet]
        [Route("Search")]
        public IEnumerable<Item> Get([FromQuery] QueryItems query)
        {
            List<Item> tmpList = null;
            if (query.SearchName != null)
                tmpList = Data.FindAll(p => p.Name.Contains(query.SearchName));
            else
                tmpList = Data;


            List<Item> tmpList2 = null;
            if (query.Sold != null)
            {
                var itemSold = query.Sold == "true";
                tmpList2 = tmpList.FindAll(p => p.Sold == itemSold);
            }
            else
            {
                tmpList2 = tmpList;
                ;
            }

            return tmpList2;
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            Data.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            var item = Data.Find(p => p.Id == id);
            if (item != null)
            {
                item.Id = value.Id;
                item.Name = value.Name;
                item.Sold = value.Sold;
                item.Price = value.Price;
            }
        }


        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = Data.Find(p => p.Id == id);
            if (item != null) Data.Remove(item);
        }
    }
}