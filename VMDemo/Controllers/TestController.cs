using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController() { }
        [HttpGet]
        [Route("Double")]
        public int GetDouble(int num)
        {
            return num * 2;
        }
    }
}
