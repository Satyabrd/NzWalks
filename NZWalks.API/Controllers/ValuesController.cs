﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily" };
            return Ok(studentNames);
        }
    }
}

