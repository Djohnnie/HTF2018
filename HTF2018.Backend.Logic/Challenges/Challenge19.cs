using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge19 : ChallengeBase, IChallenge19
    {
        public Challenge19(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        public async Task<Challenge> GetChallenge()
        {
            Challenge challenge = await BuildChallenge(Identifier.Challenge19);
            return challenge;
        }

        protected override Question BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            question.InputValues.Add(new Value { Name = "r", Data = "###S###" });
            question.InputValues.Add(new Value { Name = "r", Data = "### ###" });
            question.InputValues.Add(new Value { Name = "r", Data = "### ###" });
            question.InputValues.Add(new Value { Name = "r", Data = "###F###" });

            return question;
        }

        protected override Answer BuildAnswer(Question question, Guid challengeId)
        {
            // TODO: Calculate answer based on question!

            return new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    // TODO: Add name-data pairs containing answers!
                }
            };
        }

        protected override Example BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value{ Name = "r", Data = "#####S#####" },
                    new Value{ Name = "r", Data = "##### #####" },
                    new Value{ Name = "r", Data = "#         #" },
                    new Value{ Name = "r", Data = "# ####### #" },
                    new Value{ Name = "r", Data = "#    ###  #" },
                    new Value{ Name = "r", Data = "#### ### ##" },
                    new Value{ Name = "r", Data = "#    ###  #" },
                    new Value{ Name = "r", Data = "# ####### #" },
                    new Value{ Name = "r", Data = "#         #" },
                    new Value{ Name = "r", Data = "##### #####" },
                    new Value{ Name = "r", Data = "#####F#####" }
                }
            };

            return new Example
            {
                Question = question,
                Answer = BuildAnswer(question, challengeId)
            };
        }

        protected override void ValidateAnswer(Answer answer)
        {
            Boolean invalid = false;

            // TODO: Do a basic validation of the answer object!
            // (Null-checks, are properties correct, but no actual functional checks)

            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }
    }
}