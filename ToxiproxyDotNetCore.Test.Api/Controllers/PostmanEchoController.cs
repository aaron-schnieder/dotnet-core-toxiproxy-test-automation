using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ToxiproxyDotNetCore.Test.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostmanEchoController : ControllerBase
    {
        private readonly ILogger<PostmanEchoController> _logger;

        public PostmanEchoController(ILogger<PostmanEchoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public PostmanEcho Get()
        {
            return new PostmanEcho() { Url = new Uri("http://localhost:5001/PostmanEcho"), Args = new Args() { Foo1 = "arg value 1", Foo2 = "arg value 2"} };
        }
    }
}
