using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge15 : ChallengeBase, IChallenge15
    {
        public Challenge15(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        public async Task<Challenge> GetChallenge()
        {
            Challenge challenge = await BuildChallenge(Identifier.Challenge15);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            // TODO: Add name-data pairs to the InputValues!

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            // TODO: Calculate answer based on question!

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    // TODO: Add name-data pairs containing answers!
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            Question question = new Question
            {
                // TODO: Add name-data pairs containing an example question based on the actual question!
            };

            return new Example
            {
                Question = question,
                Answer = await BuildAnswer(question, challengeId)
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