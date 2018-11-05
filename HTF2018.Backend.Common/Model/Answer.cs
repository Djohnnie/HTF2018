using System;
using System.Collections.Generic;

namespace HTF2018.Backend.Common.Model
{
    public class Answer
    {
        public Guid ChallengeId { get; set; }
        public List<Value> Values { get; set; }
    }
}