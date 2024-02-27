using Hall_of_fame.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hall_of_fame.Controllers
{
    [Route("/api/[controller]")]
    public class PersonsController : Controller
    {
        [HttpGet]
        public IEnumerable<Person> GetPersonsList() => Helper.GetContext().Persons.ToList();

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

        private int NextProductId()
        {
            List<Person> persons = Helper.GetContext().Persons.ToList();
            return persons.Count() == 0 ? 1 : persons.Max(w => w.Id) + 1;
        }

        [HttpPost]
        public IActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            person.Id = NextProductId();
            Helper.GetContext().Add(person);
            Helper.GetContext().SaveChanges();
            return CreatedAtAction(nameof(GetPersonsList), new { id = person.Id },
                person);
        }

        [HttpPost("AddPerson")]
        public IActionResult PostBody([FromBody] Person person) => PostPerson(person);
    }
}
