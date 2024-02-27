using Hall_of_fame.models;

namespace hall_of_fame.models
{
    public class PersonSkill
    {
        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int SkillId { get; set; }
        public Skill? Skill { get; set; }

        public byte Level { get; set; }
    }
}
