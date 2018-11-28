using System;

namespace HTF2018.Backend.Common.Model
{
    /// <summary>
    /// This object represents the status for a specific challenge. It also includes a link to the endpoint to GET and POST challenge data.
    /// </summary>
    public class Progress
    {
        /// <summary>
        /// The status for this specific challenge.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// A link to the endpoint you should use to communicate with this challenge.
        /// </summary>
        public String Entry { get; set; }
    }
}
