using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TemplateLib.Models;
using RestTemplate.DBUtil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ManageItem _data = new ManageItem();
        // NO Database local data
        //
        //private static readonly List<Item> Data = new List<Item>
        //{
        //    new Item(1, "iPhoen 4", false, 345),
        //    new Item(2, "iPhoen 4S", true, 400),
        //    new Item(3, "iPhoen 5", false, 550),
        //    new Item(4, "iPhoen 5S", false, 600),
        //    new Item(5, "iPhoen SE", true, 850),
        //    new Item(6, "iPhoen 6", false, 1100),
        //    new Item(7, "iPhoen 6S", true, 1350),
        //    new Item(8, "iPhoen 6S+", false, 1500)
        //};

        // GET: api/<ItemController>
        [HttpGet]
        [EnableCors("AnotherPolicy")]
        public IEnumerable<Item> Get()
        {
            return _data.Get();
        }
        // GET api/<ItemController>/5
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return _data.GetOneById(id);
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            _data.AddItem(value);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Item value)
        {
            return _data.UpdateItem(id, value);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _data.DeleteUser(id);
        }
    }
}