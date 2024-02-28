using Hall_of_fame.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hall_of_fame.Controllers
{
    [Route("/api/[controller]")]
    public class PersonsController : Controller
    {
        [HttpGet]
        public IEnumerable<Person> GetPersonsList() => Helper.GetContext().Persons.Include(w => w.PersonSkills).ToList();

        [HttpGet("{id}")]
        public IActionResult GetPerson(long id)
        {
            Person? person = Helper.GetContext().Persons.FirstOrDefault(w => w.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        private int NextPersonId()
        {
            List<Person> persons = Helper.GetContext().Persons.ToList();
            return persons.Count() == 0 ? 1 : persons.Max(w => w.Id) + 1;
        }

        [HttpPost("AddPerson")]
        public IActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            person.Id = NextPersonId();

            Console.WriteLine(person.PersonSkills.Count());
            Helper.GetContext().Persons.Add(person);
            Helper.GetContext().Skills.AddRange(person.Skills);
            Helper.GetContext().PersonSkills.AddRange(person.PersonSkills);
            Helper.GetContext().SaveChanges();
            return CreatedAtAction(nameof(GetPersonsList), new { id = person.Id },
                person);
        }

        [HttpPost]
        public IActionResult PostBody([FromBody] Person person) => PostPerson(person);
    }
}
