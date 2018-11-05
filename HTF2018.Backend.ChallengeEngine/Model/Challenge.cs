using System;

namespace HTF2018.Backend.ChallengeEngine.Model
{
    public class Challenge
    {
        public Guid Id { get; set; }
        public Identifier Identifier { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Question Question { get; set; }
        public Example Example { get; set; }
    }
}