using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Hall_of_fame.models
{
    public class Skill
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }

        [JsonIgnore]
        public Person? Person { get; set; }
    }
}
