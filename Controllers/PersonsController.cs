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
        public IActionResult GetPerson(int id)
        {
            Person? person = Helper.GetContext().Persons.FirstOrDefault(w => w.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            Person? person = Helper.GetContext().Persons.FirstOrDefault(x => x.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            Helper.GetContext().Persons.Remove(person);
            Helper.GetContext().SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult PutPerson(Person person)
        {
            Person? searchPerson = Helper.GetContext().Persons.FirstOrDefault(w => w.Id == person.Id);
            if (searchPerson == null)
            {
                return NotFound();
            }

            searchPerson.Name = person.Name;
            searchPerson.DisplayName = person.DisplayName;
            searchPerson.PersonSkills = person.PersonSkills;
            searchPerson.Skills = person.Skills;

            Helper.GetContext().Persons.Update(searchPerson);
            Helper.GetContext().SaveChanges();
            return Ok();
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
