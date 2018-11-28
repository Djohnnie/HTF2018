using System;

namespace HTF2018.Backend.Common.Model
{
    /// <summary>
    /// This object represents a challenge for you to solve.
    /// </summary>
    public class Challenge
    {
        /// <summary>
        /// The unique identifier for this started challenge. If you request a challenge multiple times, this identifier changes.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The identifier for this challenge. If you request a challenge multiple times, it remains unchanged.
        /// </summary>
        public Identifier Identifier { get; set; }

        /// <summary>
        /// The title for this challenge.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// The description for this challenge.
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The actual question for this challenge.
        /// </summary>
        public Question Question { get; set; }
       
        /// <summary>
        /// An example containing sample question and answer.
        /// </summary>
        public Example Example { get; set; }
    }
}