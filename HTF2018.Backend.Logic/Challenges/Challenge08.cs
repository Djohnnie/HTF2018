using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge08 : ChallengeBase, IChallenge08
    {
        public Challenge08(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        public async Task<Challenge> GetChallenge()
        {
            Challenge challenge = await BuildChallenge(Identifier.Challenge03);
            return challenge;
        }

        protected override Question BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            // TODO: Add name-data pairs to the InputValues!

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
            Question question = new Question
            {
                // TODO: Add name-data pairs containing an example question based on the actual question!
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