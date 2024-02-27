using hall_of_fame.models;
using Swashbuckle.AspNetCore;
using System.Text.Json.Serialization;

namespace Hall_of_fame.models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
  
        public virtual ICollection<PersonSkill> PersonSkills { get; set; } = new List<PersonSkill>();
    }
}
