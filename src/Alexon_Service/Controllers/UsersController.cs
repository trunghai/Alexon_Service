﻿using System;
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
        public Entity Get() //IEnumerable<User>
        {
            UserDataAccess data = new UserDataAccess(_settings.ConnectionString);
            return data.getUsers();
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User value)
        {
            UserDataAccess dataAccess = new UserDataAccess(_settings.ConnectionString);
            return Json(dataAccess.addUser(value));

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User value)
        {
            UserDataAccess dataAccess = new UserDataAccess(_settings.ConnectionString);
            return Json(dataAccess.updateUser(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserDataAccess dataAccess = new UserDataAccess(_settings.ConnectionString);
            return Json(dataAccess.deleteUser(id));
        }
    }
}
