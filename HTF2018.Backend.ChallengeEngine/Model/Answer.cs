using System;
using System.Collections.Generic;

namespace HTF2018.Backend.ChallengeEngine.Model
{
    public class Answer
    {
        public Guid ChallengeId { get; set; }
        public List<Value> Values { get; set; }
    }
}