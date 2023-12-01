using Hangfire;
using HangFireDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace HangFireDemo.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public PeopleController(ApplicationDbContext db, IBackgroundJobClient backgroundJobClient)
        {
            _db = db;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost("create")]
        public ActionResult Create(string personName)
        {
            _backgroundJobClient.Enqueue(() => CreatePerson(personName));

            return Ok();
        }

        public async Task CreatePerson(string personName)
        {
            Console.WriteLine($"Adding person {personName}");
            var person = new Person
            {
                Name = personName
            };
            _db.Add(person);

            await Task.Delay(5000);
            await _db.SaveChangesAsync();

            Console.WriteLine($"{personName} added");
        }
    }
}
