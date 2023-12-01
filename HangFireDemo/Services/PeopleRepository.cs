using Hangfire;
using HangFireDemo.Data;

namespace HangFireDemo.Services
{
    public interface IPeopleRepository {
        Task CreatePerson(string personName);
    }

    public class PeopleRepository : IPeopleRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PeopleRepository> logger;

        public PeopleRepository(ApplicationDbContext db, ILogger<PeopleRepository> logger)
        {
            _db = db;
            this.logger = logger;
        }

        public async Task CreatePerson(string personName)
        {
            logger.LogInformation($"Adding person {personName}");
            var person = new Person
            {
                Name = personName
            };
            _db.Add(person);

            await Task.Delay(5000);
            await _db.SaveChangesAsync();

            logger.LogInformation($"{personName} added");
        }
    }
}
