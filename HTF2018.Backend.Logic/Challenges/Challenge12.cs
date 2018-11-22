using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Exceptions;
using HTF2018.Backend.Common.Model;
using HTF2018.Backend.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Challenge12(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
        }

        private readonly Random _randomGenerator = new Random();
        List<string[]> _rectangleStrings = new List<string[]>
        {
            new string[0],
            new[]
            {
                ""
            },
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
            }
        };

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge12);
            return challenge;
        }

        protected override Task<Question> BuildQuestion()
        {
            var random = _randomGenerator.Next(_rectangleStrings.Count());
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "drawing",
                        Data = Join(Environment.NewLine, _rectangleStrings[random])
        },
                    new Value {Name = "drawingId", Data = $"{random+1548}"}
                }
            };

            return Task.FromResult(question);
        }

        protected override Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {

            var drawingId = int.Parse(question.InputValues.Find(e => e.Name.Equals("drawingId")).Data) - 1548;



            return Task.FromResult(new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value{Name = "rectangles", Data = $"{Rectangles.Count(_rectangleStrings[drawingId])}"}
                }
            });
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value> {
                    new Value{Name = "drawing", Data = Join(Environment.NewLine, _rectangleStrings[3])},
                    new Value{Name = "drawingId", Data = $"{3+1548}"},
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
            var invalid = answer.Values == null;
            if (answer.Values != null)
            {
                invalid = true;
            }

            if (!answer.Values.Any(x => x.Name == "rectangles"))
            {
                invalid = true;
            }

            if (answer.Values.Count(x => x.Name == "rectangles") != 1)
            {
                invalid = true;
            }

            foreach (var answerValue in answer.Values.Where(x => x.Name.Equals("rectangles")))
            {
                if (string.IsNullOrEmpty(answerValue.Data))
                    invalid = true;
            }

            if (invalid)
            {
                throw new InvalidAnswerException();
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