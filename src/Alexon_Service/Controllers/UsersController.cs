using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alexon_Service.ModelCtr;
using Alexon_Service.Models;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Alexon_Service.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private AppSettings _settings;
        public UsersController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        // GET: api/values
        [HttpGet]
        public Entity Get(String username, String password) //IEnumerable<User>
        {
            
            UserDataAccess data = new UserDataAccess(_settings.ConnectionString);
            return data.login(username, password);
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Entity Post([FromBody]string value)
        {
            Console.WriteLine(value);
            UserDataAccess data = new UserDataAccess("Server=ADMIN-PC;Database=Alexon;User ID=sa;Password=hongvan@1991;Trusted_Connection=False;");
            return data.login("admin", "123456");
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
