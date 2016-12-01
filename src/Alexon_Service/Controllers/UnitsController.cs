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
    public class UnitsController : Controller
    {

        private AppSettings _settings;
        public UnitsController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        // GET: api/values
        [HttpGet]
        public Entity Get()
        {
            UnitDataAccess dataAccess = new UnitDataAccess(_settings.ConnectionString);
            return dataAccess.getUnit();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Entity Post([FromBody]Unit value)
        {
            UnitDataAccess dataAccess = new UnitDataAccess(_settings.ConnectionString);
            return dataAccess.addUnit(value);
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Entity Put(int id, [FromBody]Unit value)
        {
            UnitDataAccess dataAccess = new UnitDataAccess(_settings.ConnectionString);
            return dataAccess.updateUnit(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Entity Delete(int id)
        {
            UnitDataAccess dataAccess = new UnitDataAccess(_settings.ConnectionString);
            return dataAccess.deleteUnit(id);
        }
    }
}
