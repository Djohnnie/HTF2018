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
using HTF2018.Backend.Logic.Challenges.Helpers;
using ZXing;
using ZXing.CoreCompat.System.Drawing;
using ZXing.QrCode;
using static System.String;
using BarcodeReader = ZXing.CoreCompat.System.Drawing.BarcodeReader;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge10 : ChallengeBase, IChallenge10
    {
        private readonly IHtfContext _htfContext;
        private readonly IImageLogic _imageLogic;

        public Challenge10(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic,
            IDashboardLogic dashboardLogic, IHistoryLogic historyLogic, IImageLogic imageLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic)
        {
            _htfContext = htfContext;
            _imageLogic = imageLogic;
        }

        private readonly Random _randomGenerator = new Random();
        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge10);
            return challenge;
        }

        protected override async Task<Question> BuildQuestion()
        {
            var data = EncodeQrCode($"{RandomStrings.ArtifactSentences[_randomGenerator.Next(RandomStrings.ArtifactSentences.Count)]}");
            var image = await _imageLogic.StoreImage(data);

            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "id",
                        Data = $"{image.Id}"
                    },
                    new Value
                    {
                        Name = "image",
                        Data = $"{_htfContext.HostUri}/images/{image.Id}"
                    }
                }
            };

            return question;
        }

        protected override async Task<Answer> BuildAnswer(Question question, Guid challengeId)
        {
            var decoded = await GetDecoded(question);

            return new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value{ Name = "decoded", Data = $"{decoded}" }
                }
            };
        }

        protected override async Task<Example> BuildExample(Guid challengeId)
        {
            var data = EncodeQrCode("Your world is ours!!!");
            var image = await _imageLogic.StoreImage(data);

            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "id",
                        Data = $"{image.Id}"
                    },
                    new Value
                    {
                        Name = "image",
                        Data = $"{_htfContext.HostUri}/images/{image.Id}"
                    }
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
            if (!answer.Values.Any(x => x.Name == "decoded"))
            {
                throw new InvalidAnswerException();
            }
            if (answer.Values.Count(x => x.Name == "decoded") != 1)
            {
                throw new InvalidAnswerException();
            }
            if (string.IsNullOrEmpty(answer.Values.Single(x => x.Name == "decoded").Data))
            {
                throw new InvalidAnswerException();
            }
        }

        private async Task<string> GetDecoded(Question question)
        {
            var imageId = question.InputValues.Single(x => x.Name == "id").Data;
            var image = await _imageLogic.LoadImage(new Guid(imageId));
            return DecodeQrCode(image.Data);
        }

        private Byte[] EncodeQrCode(String input)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = 100,
                    Height = 100,
                }
            };

            var qrCodeImage = writer.Write(input);

            using (var stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                return stream.GetBuffer();
            }
        }

        private String DecodeQrCode(Byte[] input)
        {
            var reader = new BarcodeReader();

            using (var stream = new MemoryStream(input))
            {
                using (var image = (Bitmap)Image.FromStream(stream))
                {
                    return reader.Decode(image).Text;
                }
            }
        }
    }
}