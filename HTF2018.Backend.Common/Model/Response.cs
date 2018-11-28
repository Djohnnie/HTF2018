using System;

namespace HTF2018.Backend.Common.Model
{
    /// <summary>
    /// This object represents a unified response for all challenges you try to solve.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// The identifier for the challenge you have tried to solve.
        /// </summary>
        public Identifier Identifier { get; set; }

        /// <summary>
        /// The status of the challenge you have tried to solve. Now you know if your attempt was successful or not.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Your personal identification relayed to you.
        /// </summary>
        public String Identification { get; set; }

        /// <summary>
        /// Your personal overview of all challenges.
        /// </summary>
        public Overview Overview { get; set; }
    }
}