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
using ZXing;
using ZXing.CoreCompat.System.Drawing;
using ZXing.QrCode;
using BarcodeReader = ZXing.CoreCompat.System.Drawing.BarcodeReader;
using static System.String;

namespace HTF2018.Backend.Logic.Challenges
{
    public class Challenge10 : ChallengeBase, IChallenge10
    {
        public Challenge10(IHtfContext htfContext, ITeamLogic teamLogic, IChallengeLogic challengeLogic, IDashboardLogic dashboardLogic, IHistoryLogic historyLogic)
            : base(htfContext, teamLogic, challengeLogic, dashboardLogic, historyLogic) { }

        public async Task<Challenge> GetChallenge()
        {
            var challenge = await BuildChallenge(Identifier.Challenge10);
            return challenge;
        }

        protected override Question BuildQuestion()
        {
            var question = new Question
            {
                InputValues = new List<Value>()
                {
                    new Value
                    {
                        Name = "image",
                        Data = ToBase64(EncodeQrCode($"{Guid.NewGuid()}"))
                    }
                }
            };

            return question;
        }

        protected override Answer BuildAnswer(Question question, Guid challengeId)
        {
            var decoded = GetDecoded(question);

            return new Answer
            {
                ChallengeId = challengeId,
                Values = new List<Value>
                {
                    new Value{ Name = "decoded", Data = $"{decoded}" }
                }
            };
        }

        protected override Example BuildExample(Guid challengeId)
        {
            var question = new Question
            {
                InputValues = new List<Value>
                {
                    new Value
                    {
                        Name = "image",
                        Data = ToBase64(EncodeQrCode($"{Guid.NewGuid()}"))
                    }
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
            var invalid = answer.Values == null;
            if (answer.Values != null && answer.Values.Count != 1) { invalid = true; }
            if (!answer.Values.Any(x => x.Name == "decoded")) { invalid = true; }
            if (IsNullOrEmpty(answer.Values.Single(x => x.Name == "decoded").Data)) { invalid = true; }

            if (invalid)
            {
                throw new InvalidAnswerException();
            }
        }

        private string GetDecoded(Question question)
        {
            var qrCode = question.InputValues.Single(x => x.Name == "image").Data;
            return DecodeQrCode(FromBase64(qrCode));
        }

        private String ToBase64(Byte[] input)
        {
            return Convert.ToBase64String(input);
        }
        private Byte[] FromBase64(String input)
        {
            return Convert.FromBase64String(input);
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