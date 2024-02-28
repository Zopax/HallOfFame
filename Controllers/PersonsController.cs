using Hall_of_fame.models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace hall_of_fame.Controllers
{
    [Route("/api/[controller]")]
    public class PersonsController : Controller
    {
        [HttpGet]
        public IEnumerable<Person> GetPersonsList() => Helper.GetContext().Persons.ToList();

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

            

            Helper.GetContext().Persons.Update(searchPerson);
            Helper.GetContext().SaveChanges();
            return Ok();
        }

        private int NextObjectId(List<Person> person)
        {
            return person.Count() == 0 ? 1 : person.Max(w => w.Id) + 1;
        }

        private int NextObjectId(List<Skill> skill)
        {
            return skill.Count() == 0 ? 1 : skill.Max(w => w.Id) + 1;
        }

        [HttpPost("AddPerson")]
        public IActionResult PostPerson([FromForm]Person personInput)
        {

            foreach (var s in personInput.Skills)
            {
                Console.WriteLine(s.Name);
            }

            try
            {
                Person newPerson = new Person
                {
                    Id = NextObjectId(Helper.GetContext().Persons.ToList()) + 1,
                    Name = personInput.Name,
                    DisplayName = personInput.DisplayName,
                    Skills = personInput.Skills,
                };

               // Helper.GetContext().Persons.Add(newPerson);
                //Helper.GetContext().SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
