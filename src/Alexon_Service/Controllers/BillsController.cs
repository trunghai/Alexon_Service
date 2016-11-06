using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alexon_Service.Models;
using Microsoft.Extensions.Options;
using Alexon_Service.ModelCtr;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Alexon_Service.Controllers
{
    [Route("api/[controller]")]
    public class BillsController : Controller
    {

        private AppSettings _settings;
        public BillsController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }


        // GET: api/values
        [HttpGet]
        public Entity Get()
        {
            BillDataAccess dataAccess = new BillDataAccess(_settings.ConnectionString);
            return dataAccess.getBills();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Bill value)
        {
            BillDataAccess dataAccess = new BillDataAccess(_settings.ConnectionString);
            return Json(dataAccess.addBill(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
