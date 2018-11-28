using System.Collections.Generic;

namespace HTF2018.Backend.Common.Model
{
    /// <summary>
    /// This object represents a question containing all the needed data.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// The data you need to solve the challenge question.
        /// </summary>
        public List<Value> InputValues { get; set; }
    }
}