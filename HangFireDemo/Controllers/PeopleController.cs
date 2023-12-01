using Hangfire;
using HangFireDemo.Data;
using HangFireDemo.Services;
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

        [HttpPost(Name = "create")]
        public ActionResult Create(string personName)
        {
            _backgroundJobClient.Enqueue<IPeopleRepository>(repo => repo.CreatePerson(personName));

            return Ok();
        }


       
    }
}
