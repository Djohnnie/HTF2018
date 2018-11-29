using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge06 : ChallengeBase, IChallenge06
    {
        public Challenge06(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        private readonly Random _randomGenerator = new Random();
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge06);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var startDate = RandomDate();
            var dayOfTheWeek = _randomGenerator.Next(1, 8);
            var question = new Question
            {
                InputValues = new List<Value>()
                {
                    new Value
                    {
                        Name = "startDate",
                        Data = $"{startDate}"
                    },
                    new Value
                    {
                        Name = "endDate",
                        Data = $"{RandomDate(startDate)}"
                    },
                    new Value
                    {
                        Name = "day",
                        Data = $"{(DayOfWeek)dayOfTheWeek}"
                    }
                }
            };
            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var startDate =DateTime.Parse(question.InputValues.Single(e => e.Name.Equals("startDate")).Data);
            var endDate = DateTime.Parse(question.InputValues.Single(e => e.Name.Equals("endDate")).Data);

            var dayOfTheWeekParsed = Enum.TryParse(question.InputValues.Single(e => e.Name.Equals("day")).Data, out DayOfWeek dayOfTheWeek);
            
            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value
                    {
                        Name = "count",
                        Data = $"{CountDaysBetweenDates(startDate,endDate,dayOfTheWeek)}"
                    }
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value> {
                    new Value{Name = "startDate", Data =$"{new DateTime(1994,11,06)}"},
                    new Value{Name = "endDate", Data =$"{new DateTime(1994,11,25)}"},
                    new Value{Name = "day", Data =$"{DayOfWeek.Monday}"},
                }
            };

            return new Example
            {
                Question = question,
                Answer = await BuildAnswer(question, challengeId)
            };
        }

        protected override void ValidateAnswer(Answer answer)
        {
            if (answer.Values == null)
            {
                throw new InvalidAnswerException();
            }
            if (!answer.Values.Any(x => x.Name == "count"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "count") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "count").Data))
            {
                throw new InvalidAnswerException();
            }
        }

        private DateTime RandomDate(DateTime? after=null)
        {
            var baseDate = new DateTime(1990, 1, 1);
            var range = (DateTime.Today - baseDate).Days;
            if (after.HasValue)
            {
                baseDate = after.Value;
                range = 5000;
            }
            return baseDate.AddDays(_randomGenerator.Next(range));
        }
        private int CountDaysBetweenDates(DateTime startDate, DateTime endDate, DayOfWeek weekDay)
        {
            var dayCount = 0;

            for (var dt = startDate; dt < endDate; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == weekDay)
                {
                    dayCount++;
                }
            }

            return dayCount;
        }
    }
}