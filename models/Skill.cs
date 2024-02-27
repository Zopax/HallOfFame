using hall_of_fame.models;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Hall_of_fame.models
{
    public class Skill
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
        [JsonIgnore]
        public virtual ICollection<PersonSkill> PersonSkills { get; set; } = new List<PersonSkill>();
    }
}
