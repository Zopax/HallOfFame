using Microsoft.AspNetCore.Mvc;

namespace Hall_of_fame.models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
  
    }
}
