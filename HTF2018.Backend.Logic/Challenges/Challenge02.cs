using System;
using System.Collections.Generic;
using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 02:
    ///   Calculate the sum of all given integer values.
    /// </summary>
    public class Challenge02 : IChallenge
    {
        public Challenge GetChallenge()
        {
            return new Challenge
            {
                Id = Guid.NewGuid(),
                Identifier = Identifier.Challenge01,
                Title = "A simple challenge to get started!",
                Description = "Calculate the sum of all given integers",
                Question = BuildQuestion(),
                Example = BuildExample()
            };
        }

        public Response ValidateChallenge(Answer answer, IHtfContext context)
        {
            throw new NotImplementedException();
        }

        private Question BuildQuestion()
        {
            return new Question();
        }

        private Example BuildExample()
        {
            return new Example
            {
                Question = new Question
                {
                    InputValues = new List<Value>
                    {
                        new Value{ Name = "i", Data = "385" },
                        new Value{ Name = "i", Data = "8269" },
                        new Value{ Name = "i", Data = "52" },
                    }
                },
                Answer = new Answer
                {
                    Values = new List<Value>
                    {
                        new Value{ Name = "sum", Data = "8706" }
                    }
                }
            };
        }
    }
}