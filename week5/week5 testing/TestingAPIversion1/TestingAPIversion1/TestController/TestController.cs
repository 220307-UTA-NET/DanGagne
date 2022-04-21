using System.Text.Json;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using TestingAPIDatabases;


namespace TestingAPIversion1.TestController
{
    //ASP.NET uses "reflection" to automatically find classes that follow a naming convention
    //("________Controller")

    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : Controller
    {
       

        private static readonly List<int> s_sample = new() {  };
        private static readonly List<string> s_sample2 = new() { };
        private static readonly List<Customer> s_customer = new() { };

        private readonly ILogger<SampleController> logger;

        public readonly string connString = System.IO.File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");



        public SampleController(ILogger<SampleController> logger)
        {
            this.logger = logger;
            
        }

        [HttpPost("/customer/add")]
        public ContentResult AddCustomer([FromBody] Customer newcust)
        {
            SqlRepo sqlrepo = new SqlRepo(connString);
            string json = sqlrepo.AddOneCustomer(newcust);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
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

        [HttpGet("/customer/id")]
        public ContentResult GetCustomer( int ID)
        {
            s_customer.Clear();
            SqlRepo sqlrepo = new SqlRepo(connString);
           foreach( Customer c in sqlrepo.GetCustomer(ID))
            {
                s_customer.Add(c);
            }
         
            string json = JsonSerializer.Serialize(s_customer);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpGet("/customer/all")]
        public ContentResult GetAllCustomers()
        {
            s_customer.Clear();
            SqlRepo sqlrepo = new SqlRepo(connString);
            foreach (Customer c in sqlrepo.AllCustomers())
            {
                s_customer.Add(c);
            }

            string json = JsonSerializer.Serialize(s_customer);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpDelete("/customer/deleteOne/id")]
        public ContentResult DeleteOneSample( int ID)
        {
            SqlRepo sqlrepo = new SqlRepo(connString);
            string json = sqlrepo.DropOneCustomer(ID);

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
