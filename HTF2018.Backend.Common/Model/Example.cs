namespace HTF2018.Backend.Common.Model
{
    /// <summary>
    /// This object represents an example challenge for you to learn from.
    /// </summary>
    public class Example
    {
        /// <summary>
        /// The example question that is applicable to a specific challenge.
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// The example answer that is applicable to a specific challenge and is calculated based on the example question.
        /// </summary>
        public Answer Answer { get; set; }
    }
}