﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PlanRestApi.Models;
using PlanRestApi.Repositories;

namespace PlanRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypePlanController : ControllerBase
    {
        private readonly TypePlanRepository _typePlanRepository;
        public TypePlanController(IConfiguration configuration)
        {
            _typePlanRepository = new TypePlanRepository(configuration);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _typePlanRepository.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var model = _typePlanRepository.Get(id);
            if(model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Insert([Bind("Name")]TypePlan type)
        {
            if (ModelState.IsValid)
            {
                var result = _typePlanRepository.Insert(type);
                var lastType = result ? _typePlanRepository.GetLastInserted() : null;
                var uri = Url.Action("Get", new { id = lastType.Id });
                return Created(uri, lastType);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([Bind("Id,Name")]TypePlan type)
        {
            if (ModelState.IsValid)
            {
                _typePlanRepository.Update(type);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _typePlanRepository.Get(id);
            if(model == null)
            {
                return NotFound();
            }
            _typePlanRepository.Delete(id);
            return NoContent();
        }
    }
}