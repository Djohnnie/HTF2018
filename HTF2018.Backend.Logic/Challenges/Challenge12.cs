using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.String;

namespace HTF2018.Backend.Logic.Challenges
{
    /// <summary>
    /// CHALLENGE 07:
    ///   Are they playing with us?
    /// </summary>
    public class Challenge12 : ChallengeBase, IChallenge12
    {
        private readonly IHtfContext _htfContext;
        private readonly IImageLogic _imageLogic;

        public Challenge12(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic, IImageLogic imageLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _htfContext = htfContext;
            _imageLogic = imageLogic;
        }

        private readonly Random _randomGenerator = new Random();
        List<string[]> _rectangleStrings = new List<string[]>
        {
            new[]
            {
                " "
            },
            new[]
            {
                "+-+",
                "| |",
                "+-+"
            },
            new[]
            {
                "  +-+",
                "  | |",
                "+-+-+",
                "| |  ",
                "+-+  "
            },
            new[]
            {
                "  +-+",
                "  | |",
                "+-+-+",
                "| | |",
                "+-+-+"
            },
            new[]
            {
                "+--+",
                "+--+"
            },
            new[]
            {
                "++",
                "||",
                "++"
            },
            new[]
            {
                "++",
                "++"
            },
            new[]
            {
                "  +-+",
                "    |",
                "+-+-+",
                "| | -",
                "+-+-+"
            },
            new[]
            {
                "+------+----+",
                "|      |    |",
                "+---+--+    |",
                "|   |       |",
                "+---+-------+"
            },
            new[]
            {
                "+------+----+",
                "|      |    |",
                "+------+    |",
                "|   |       |",
                "+---+-------+"
            },
            new[]
            {
                "+---+--+----+",
                "|   +--+----+",
                "+---+--+    |",
                "|   +--+----+",
                "+---+--+--+-+",
                "+---+--+--+-+",
                "+------+  | |",
                "          +-+"
            }, new[]
            {
                "+---+--+----+",
                "|   +--+----+",
                "+---|  |    |",
                "|   +--+----+",
                "+---| -+--+-+",
                "+---| -+--+-+",
                "+------+  | |",
                "          +-+"
            }
        };

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge12);
            return challenge;
        }

        protected async override Task<Question> BuildQuestion()
        {
            var random = _randomGenerator.Next(_rectangleStrings.Count());
            var drawing = _rectangleStrings[random];
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            foreach (var line in drawing)
            {
                question.InputValues.Add(new Value { Name = "d", Data = line });
            }

            var imageBytes = BuildDrawingImage(drawing);
            var image = await _imageLogic.StoreImage(imageBytes);
            question.InputValues.Add(new Value { Name = "graphic", Data = $"{_htfContext.HostUri}/images/{image.Id}" });

            return question;
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var drawing = new List<string>();
            foreach (var value in question.InputValues)
            {
                if (value.Name == "d")
                {
                    drawing.Add(value.Data);
                }
            }

            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value { Name = "rectangles", Data = $"{Rectangles.Count(drawing.ToArray())}" }
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var drawing = _rectangleStrings[3];
            var question = new Question
            {
                InputValues = new List<Value>()
            };

            foreach (var line in drawing)
            {
                question.InputValues.Add(new Value { Name = "d", Data = line });
            }

            var imageBytes = BuildDrawingImage(drawing);
            var image = await _imageLogic.StoreImage(imageBytes);
            question.InputValues.Add(new Value { Name = "graphic", Data = $"{_htfContext.HostUri}/images/{image.Id}" });

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
            if (!answer.Values.Any(x => x.Name == "rectangles"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "rectangles") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "rectangles").Data))
            {
                throw new InvalidAnswerException();
            }
        }

        private byte[] BuildDrawingImage(string[] drawing)
        {
            var empty = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyhpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTMyIDc5LjE1OTI4NCwgMjAxNi8wNC8xOS0xMzoxMzo0MCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUuNSAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NUFBRDRFNjA1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NUFBRDRFNjE1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo1QUFENEU1RTVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo1QUFENEU1RjVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pn8auUcAAADPSURBVHja7JlLCoAwDAX9Igiew2t5Qi8nCCKKO+mmUGmiCU5WrooD81KeluM8FZ6nKpwPAF9P4/Glj2VDIQBMZ6Dq2/v5XHfx8+uhQyEA1DMQ7trQuZTR8D6WKxQCQCsDMe9DF7W9TzkfhQCw3gdy7hMUAsBiBp7uew3vUQiAtzOg3XFRCADrGbDmPQoBQB9AIQD8ZIA+AMBfMvB030t1DBQCQDIDOV5K/UcLs5RyPgoBIJkBy90XhQDw2ge0+wMKAeApAxrfiFAIgMy5BBgAWcs64FTfoyoAAAAASUVORK5CYII=";
            var corner = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyhpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTMyIDc5LjE1OTI4NCwgMjAxNi8wNC8xOS0xMzoxMzo0MCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUuNSAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NUFCOTg2NUM1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NUFCOTg2NUQ1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo1QUI5ODY1QTVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo1QUI5ODY1QjVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PrQkCMkAAASFSURBVHja7Fq9UtwwELZ+fD/cUWUm10ORjj709HmAzCRdHiBvkJKZtHkC6rwAPfT0oWeSIgMcd2dbUj55fT4DtrHluxNk0GgYJhmZ/bS7n75dib37+TnoMKLfN4EJjvvxgTBt136ahVeGid2BGIbOBvCg2xA7Pfw8iYXD2o+hwk81XXQxoCsAPrCbd6H5lV7947DP81mz9kjqCTOBNnoRewPABOcD6ewEYLBOmCXeANhPDG0UnSmXTx0JG0UmSoxukULqZpHPdQAIBZN8GrCzhLVdO+HBHrNO0IvEmwfyTDhfOmG20PlsGEW+AfSlcxQdCgvAxF4BIJUDzhBFv7RLFFkuAhvFqil37/bzuR4Adid6loUuFHNYu8dtBptEe/OAdYK0AHCyOqzdzwAovwDspy61C4ADrp09IGu4thhzjdLA1QPZMHVHAd9Z6SV9F2/AAx0AkBD0nAO+xosHIGu49slYLI5iXG5iVH2/EoBRWkcq0FrHuphe7Jo9FhEkSJuMosBuIjTaeQBnocFMVGp6OSeY+xuh55kEYKHwFkJ2sxcJpikc5hCJOORxxOCXcQW1gHPs1GlBk659L1w2NRMgTgwsk+tZvoujwEAb4lgBr40bfS7z0pVWRKAkCtqOqVmd5a0BkPUwGqaTsnUYVpAFjUwvjfulB1q4wEZ7ovU8lqiJUFxPvNIpeY+HjYzQUaKmUR7t8mtfBb4HaVhSU+WRqo2llnmcswtFO1K0hP5uZyuiGXfo2DTP4EvDUx6T5TweqSK7oHiA6R+koiyV3rd/usyd+M9tvWIFtSDgH0S7fwAwC8aBiB9WOSl90/+C3KpY0T8AjOOBe1+oBMAW4n4bYm6bowttvNYDrwD+hxwYe7zgeA2hZxZCW9ZRdQBujW2zQWlB6z7ut0FRfempMXt+HoCyPVMcP0kh1qnIOfsxTDZqXJMOgMztPk34qbpX1KFOtz1nzpl4uNXJ3xkQwj/7TjXkOj0Au09isWoJwtphmJpe13GhXx5bv30dJb9HkuzGZotRn1qcT1TyaR+c7rY8Dmw9Asda3/aunLqwh3LjwVMV98Wokb23u22/ayu9rXsA+Yb9PgfBLEsf1NCY7udAzqGtuoWw49tC0uahMEcWUW+9tH1mqTyliisdFFkRKSpGPcpSFwBUYrv1sHLrbTUcsAvsawb5qa4Ws50vPgiRq8VEXYMHHFpA4ZuR2O3rKDGR7U9hRygsS6QOeJwxy4oVvVc/apS20MZAGgb6LhajdZzEVfdQblzxBBNUfL94N1cclfcVr3L6JQJgGSE6/1Wk70YAIC7zWWd/2sifOh3EdKft3QPM2QMjtjpJvAGgRn6XZxH24tAjALrPc3yYwnTg+q5jbQcZ5QDECbRK2/M4e16qDYqKUune5F1GZw9wRrcpDk4A4I6P5NZzDtAbs/NHb8xuZ3E+q9Zmj+TmsU8ArNf5kRw0nNL+PLB8aoniqO3aCc/uw9fiBHcpQa8kHAAEy/eu6i7yDYA9fDU9Hob5rIsiaeiloprF3gCAiyiVPT5dx/gnwACvhEYtZhdK4gAAAABJRU5ErkJggg==";
            var horizontalLine = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyhpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTMyIDc5LjE1OTI4NCwgMjAxNi8wNC8xOS0xMzoxMzo0MCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUuNSAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NUFCOTg2NTg1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NUFCOTg2NTk1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo1QUIyMzE5ODVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo1QUI5ODY1NzVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PqSyHE8AAANdSURBVHja7Fq7jtNAFJ2X8yBBotsaGrr9APqUK61ES0HFN4FoEKJG9NtR8AHbUUC/7YZsnHgenJvJOk5wvE7icTZiRqNZK1mP75l77p17xuEvv75lp9wEO/EWARy7qVM02oxnkUIRwKOOAfEkya/tXdb4/PJpN1IoAggeA8VcW+RcnRaC99viKlIoAggVA9t4X+RiaN7XmX8rAGesnRtmrc0scy7/nN/y1bUSXEmMoqcehQdsZhy6NgvTXTmwbQstuOhInigapWgVAC32TKPD+vyLM+7Qz6XzF6U33ziOfm1ovLHMppqlGlPwRIpeIvtJGwD07ZQefN9G0sDoc2HPai3iAtjCTgC4tgJgrozEQhj0cUrUcpwLvt9+Uqfx5+9f4w8sHinqjUx6pQU68CyfkUguwDDeIAAfojbN+LuPl28SU73ef6Yr4g9rEwM++ZJJOOQ+4qV61s+9sb/p1tnp3Ezmy2l/fr548J79AOTe8LxaPA3eULKvRHef8LCzzEwRqNoz17M9ePpbMJONjIU3QCo313qumZjVTFmeKjC6mBhBeLAG1rfhgbV5HPumJXyCrFUIQ2JXufVIaYX8h2SI5bhUZli8+18A/e5qSaYzG8ItCA+QCuMPIyasKioGzL2SlBJBmNJAPQ6ADbf8tuUYXgg3fCjmj38uBBM9m2M12iJtoh6IACKACKDlfWC/zbFOCaOqN0iUX78sn2zZZ0isCSqt/MXxPQCjfaWFsbpEWTUSodKDARLsqU2porqKzNdC3vQP2QoPyaj80CEprxbpEEBbOrmgitcUdSlgFAuEcBTi3z9drHQTZ6KroMfJ7t2l04bggE9QtYd2yFITL03vd7Yt9m6SL83M3dzrD6+bQsDwWo8AiJ6Sw97hanUzOqaZGad5uDfoDZgO1nhVxIP+1MALQjOZ5WrwEIesyev8KK0pANXvE+AN8KoY6D5lVadgSDY6OFscN+WHND7ByEFHdFR7G5ns00EdOSTNfMois+wyBdeN10TSkndUMVBbVWRcCjnoygFRiyfCpprcoi3l4tL/V4IJJHEa5a47cWgkSNbo2CHYYUfzawAOeb/b1Hu0YizVmT+W0xFAk9Vo6Pdc0QMRwP+ricPtJ5FCEcApxcAhv0GKFIoAArW/AgwAh+LY2sEc8KMAAAAASUVORK5CYII=";
            var verticalLine = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyhpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTMyIDc5LjE1OTI4NCwgMjAxNi8wNC8xOS0xMzoxMzo0MCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUuNSAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NUFCMjMxOTY1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NUFCMjMxOTc1RURDMTFFNjkxQThEQUIzRDFGRjQyOTIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo1QUIyMzE5NDVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo1QUIyMzE5NTVFREMxMUU2OTFBOERBQjNEMUZGNDI5MiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PgAQRq8AAAP2SURBVHja1Fq9UhsxENbPHbYx1PRQpKMPPW+RmeR58hypkwdwb3p63DMpiTH2naSstPbhGNuRVmsLdjwMw4wOf7vfrr7dW/np5zeRYYvfT8KJ773mWrvUs19n9aOT+ryvBzX5CyiRZ/r0BH6OWspzvtQGfprpPOcL5AJQfe+8kdGPNvnsbWUvpBPW2XlTDIDUSvUr+OVXqwnHAYMPwqwtBsA/YkBn0a32LHKL1tmEFDJP8+7DAaDWslJTIcetTD17ocSl9EGw87ZYBLpMuDNRT/sza7pPxyLXlAXQ82kwNpSn3eh3EAFIZaEksOjBUljkaxFUo8bE1u7zXvfhAeA9ceKr0L2RhLOXymeway3hbMUFQFYAoJ1ERODszb17pdwdeN/aYhQKAPyjQBoQzl6rkAYLwxkBKLHrnIuLgLi3bB7Z9PTpa9zsc3MACql9vh+sJdtsvkkVFIKuMSUpFECEMu/EMY0TALJoYuUxAVR7au1/ubhu67w8hO16fsX4P5zx5B5uC8Bb3r9HComgKK+U+6g5QLalAFGyJABUMl7VpNvUrQRVyQgEIUADgBGQumgEUIoRZhOdAMEqXI5CQclge5VqqGFRTR1cjW6tx9DRohAgRABu7olTQZBXxSKA/RSIyjNJdX+taf+aE8BnTeEPdtLYlJYB4PmzaLvuNtXGeQCiju3S4su/vDTo/ot0b4xaP4+B9KVdAjwRMM8LsRpREQCI1WisDAAza0ACwf11UyXXn0cbOjhJ5w8HgDBbxjlzqv1oNLJfKnoLEQV9lxbv3I/TtdTqOTIegB72cjyoSrkf2a+HJ+T0zQVgoXRS3Q/s9+6XWemb25Fh9dz49vunD53hywTVr3PYnxUBuLzsS5tZPfH1VBkAqB1AexIur3ErMy8vjgiEcT6B/a/ip18LDqtyIvBW/MRMHzLFD0MEfPvrfPdI4A90j4z8IUYAu8fL1fgksvKwqH+mHAj9O23+g4NHWvvLR6HQ/uJQn9i/a7b5KedoMUoC2WX7u7/HOHAO/Nu/H27uedSxSuQAwpv8sADYM3iTQly8zOoxnrZv3+x8X3HMCODkFF8jfEgKLW9u6w5FoWjUEr4EiIL1uww3N9DOBvXRnKI2eNl99pxBGTMl+XG5XEN6o8pGIbxHH0hvI1cssmUB0LcKrjL2OthyALXkxmJKJO+DgtKgpvSwHAAc5E+cgps1dZ7ebRVAV721o4/Zy2Aoo1uDEGk4hSfvmPEAwIbwLmPHLGfVkg0AbUluGYHEVUtuCmmFq5aEdVFIG5wm4WismJRYLh3T9l1xYTe8WCgKQPoOi7A1DbXIX8nWmVlTDAAUQcwEHPOnGsvqOthfAQYAnBPbPhbkAPsAAAAASUVORK5CYII=";
            var emptyImage = PngFromBase64(empty);
            var cornerImage = PngFromBase64(corner);
            var horizontalLineImage = PngFromBase64(horizontalLine);
            var verticalLineImage = PngFromBase64(verticalLine);

            var tokens = new char[drawing[0].Length, drawing.Length];
            int mY = 0;
            foreach (var drawingRow in drawing)
            {
                int mX = 0;
                foreach (var drawingChar in drawingRow)
                {
                    tokens[mX, mY] = drawingChar;
                    mX++;
                }
                mY++;
            }
            using (Bitmap bitmap = new Bitmap(tokens.GetLength(1) * 64, tokens.GetLength(0) * 64))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    for (int y = 0; y < tokens.GetLength(1); y++)
                    {
                        for (int x = 0; x < tokens.GetLength(0); x++)
                        {
                            if (tokens[x, y] == ' ')
                            {
                                g.DrawImageUnscaled(emptyImage, 64 * x, 64 * y);
                            }
                            if (tokens[x, y] == '+')
                            {
                                g.DrawImageUnscaled(cornerImage, 64 * x, 64 * y);
                            }
                            if (tokens[x, y] == '-')
                            {
                                g.DrawImageUnscaled(horizontalLineImage, 64 * x, 64 * y);
                            }
                            if (tokens[x, y] == '|')
                            {
                                g.DrawImageUnscaled(verticalLineImage, 64 * x, 64 * y);
                            }
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return stream.GetBuffer();
                }
            }
        }

        private Image PngFromBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
    }

    public static class Rectangles
    {
        public static int Count(string[] rows)
        {
            var grid = ParseGrid(rows);
            var corners = FindCorners(grid);
            return corners.Sum(corner => RectangleForCorner(corner, corners, grid));
        }

        private static CellType[][] ParseGrid(string[] rows)
            => rows.Select(row => row.Select(ParseCell).ToArray()).ToArray();

        private static CellType ParseCell(char cell)
        {
            switch (cell)
            {
                case '+':
                    return CellType.Corner;
                case '-':
                    return CellType.HorizontalLine;
                case '|':
                    return CellType.VerticalLine;
                case ' ':
                    return CellType.Empty;
                default:
                    throw new ArgumentException();
            }
        }

        private static int Rows(CellType[][] grid) => grid.Length;

        private static int Cols(CellType[][] grid) => grid[0].Length;

        private static CellType Cell(Point point, CellType[][] grid) => grid[point.Y][point.X];

        private static Point[] FindCorners(CellType[][] grid) =>
            Enumerable.Range(0, Rows(grid)).SelectMany(y =>
                    Enumerable.Range(0, Cols(grid)).Select(x => new Point(x, y)))
                .Where(point => Cell(point, grid) == CellType.Corner)
                .ToArray();

        private static bool ConnectsVertically(Point point, CellType[][] grid) =>
            (Cell(point, grid) == CellType.VerticalLine) ||
            (Cell(point, grid) == CellType.Corner);

        private static bool ConnectedVertically(Point top, Point bottom, CellType[][] grid) =>
            Enumerable.Range(top.Y + 1, bottom.Y - top.Y - 1).All(y => ConnectsVertically(new Point(top.X, y), grid));

        private static bool ConnectsHorizontally(Point point, CellType[][] grid) =>
            (Cell(point, grid) == CellType.HorizontalLine) ||
            (Cell(point, grid) == CellType.Corner);

        private static bool ConnectedHorizontally(Point left, Point right, CellType[][] grid) =>
            Enumerable.Range(left.X + 1, right.X - left.X - 1)
                .All(x => ConnectsHorizontally(new Point(x, left.Y), grid));

        private static bool IsTopLineOfRectangle(Point topLeft, Point topRight, CellType[][] grid) =>
            (topRight.X > topLeft.X) && (topRight.Y == topLeft.Y) && ConnectedHorizontally(topLeft, topRight, grid);

        private static bool IsRightLineOfRectangle(Point topRight, Point bottomRight, CellType[][] grid) =>
            (bottomRight.X == topRight.X) && (bottomRight.Y > topRight.Y) &&
            ConnectedVertically(topRight, bottomRight, grid);

        private static bool IsBottomLineOfRectangle(Point bottomLeft, Point bottomRight, CellType[][] grid) =>
            (bottomRight.X > bottomLeft.X) && (bottomRight.Y == bottomLeft.Y) &&
            ConnectedHorizontally(bottomLeft, bottomRight, grid);

        private static bool IsLeftLineOfRectangle(Point topLeft, Point bottomLeft, CellType[][] grid) =>
            (bottomLeft.X == topLeft.X) && (bottomLeft.Y > topLeft.Y) && ConnectedVertically(topLeft, bottomLeft, grid);

        private static int RectangleForCorner(Point topLeft, Point[] corners, CellType[][] grid)
        {
            return (from topRight in corners.Where(corner => IsTopLineOfRectangle(topLeft, corner, grid))
                    from bottomLeft in corners.Where(corner => IsLeftLineOfRectangle(topLeft, corner, grid))
                    from bottomRight in corners.Where(corner => IsRightLineOfRectangle(topRight, corner, grid) &&
                                                                IsBottomLineOfRectangle(bottomLeft, corner, grid))
                    select 1)
                .Count();
        }

        private enum CellType
        {
            Empty,
            Corner,
            HorizontalLine,
            VerticalLine
        }
    }
}