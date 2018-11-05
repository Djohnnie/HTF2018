using System;

namespace HTF2018.Backend.Common
{
    public interface IHtfContext
    {
        String RequestUri { get; set; }
    }

    public class HtfContext : IHtfContext
    {
        public String RequestUri { get; set; }
    }
}