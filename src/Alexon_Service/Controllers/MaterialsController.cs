﻿using System;
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
    public class MaterialsController : Controller
    {

        private AppSettings _settings;
        public MaterialsController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }
        // GET: api/values
        [HttpGet]
        public Entity Get(String page, String pageSize, String codeMaterialType, String keySearch)
        {
            MaterialDataAccess dataAccess = new MaterialDataAccess(_settings.ConnectionString);
            return dataAccess.getMaterials(page, pageSize, codeMaterialType, keySearch);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Material value)
        {
            MaterialDataAccess dataAccess = new MaterialDataAccess(_settings.ConnectionString);
            return Json(dataAccess.addMaterial(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Material value)
        {
            MaterialDataAccess dataAccess = new MaterialDataAccess(_settings.ConnectionString);
            return Json(dataAccess.updateMaterial(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            MaterialDataAccess dataAccess = new MaterialDataAccess(_settings.ConnectionString);
            return Json(dataAccess.deleteMaterial(id));
        }
    }
}
