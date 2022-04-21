using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace myFirstServerDan.SampleController
{
    //ASP.NET uses "reflection" to automatically find classes that follow a naming convention
    //("________Controller")

    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : Controller
    {
       // a controllers job is to handle a subset of requests to the server
       //based on the URL path and the HTTP method

        //each category of request (GET, PUT, etc) with the same things to do will be one method in this class

        private static readonly List<int> s_sample = new() { 12 };

        private readonly ILogger<SampleController> logger;

        public SampleController(ILogger<SampleController> logger)
        {
            this.logger = logger;
        }


        [HttpGet("/sample")]
        public ContentResult GetSample()
        {
            string json = JsonSerializer.Serialize(s_sample);

            var result = new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            logger.LogInformation("Get Request Processed");

            return result;

        }

        [HttpPost("/sample")]
        public ContentResult AddSample([FromBody] int sample)
        {
            s_sample.Add(sample);
            string json = JsonSerializer.Serialize(s_sample);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpDelete("/sample/deleteOne")]
        public ContentResult DeleteOneSample([FromBody] int sample)
        {
            s_sample.Remove(sample);
            string json = JsonSerializer.Serialize(s_sample);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }


        [HttpDelete("/sample/deleteAll")]
        public ContentResult DeleteAllSample()
        {
            s_sample.Clear();
            string json = JsonSerializer.Serialize(s_sample);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpPut("/sample")]
        public ContentResult UpdateSample([FromBody] List<int> sample)
        {
            s_sample.Clear();
            foreach (int sampleItem in sample)
            {
                s_sample.Add(sampleItem);
            }
            string json = JsonSerializer.Serialize(s_sample);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json               
            };
        }
    }
}
