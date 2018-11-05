using HTF2018.Backend.Common;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.Logic.Challenges
{
    public class ChallengeBase
    {
        private readonly IHtfContext _htfContext;

        protected ChallengeBase(IHtfContext htfContext)
        {
            _htfContext = htfContext;
        }

        protected Overview BuildOverview()
        {
            return new Overview
            {
                Challenge01 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge02 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge03 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge04 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge05 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge06 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge07 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge08 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge09 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge10 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge11 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge12 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge13 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge14 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge15 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge16 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge17 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge18 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge19 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
                Challenge20 = new Progress { Entry = _htfContext.RequestUri, Status = Status.Unknown },
            };
        }
    }
}