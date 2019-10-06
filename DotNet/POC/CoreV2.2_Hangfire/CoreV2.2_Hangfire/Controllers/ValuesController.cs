using System;
using System.Collections.Generic;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace CoreV2._2_SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBackgroundJobClient backgroundJobs;

        public ValuesController(IBackgroundJobClient _backgroundJobs)
        {
            backgroundJobs = _backgroundJobs;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire in controller!"));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
