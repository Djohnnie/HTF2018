using System;
using System.Collections.Generic;

namespace HTF2018.Backend.Common.Model
{
    /// <summary>
    /// This object represents an answer containing all the provided data.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// The unique identifier for the challenge you are providing an answer for.
        /// </summary>
        public Guid ChallengeId { get; set; }

        /// <summary>
        /// The data you need to provide to solve the challenge question.
        /// </summary>
        public List<Value> Values { get; set; }
    }
}