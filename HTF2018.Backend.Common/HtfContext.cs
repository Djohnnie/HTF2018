﻿using HTF2018.Backend.Common.Model;
using System;

namespace HTF2018.Backend.Common
{
    public interface IHtfContext
    {
        String RequestUri { get; set; }

        String HostUri { get; set; }

        Boolean IsIdentified { get; set; }

        Boolean IsIdentifiedAsAdmin { get; set; }

        String Identification { get; set; }

        Team Team { get; set; }
    }

    public class HtfContext : IHtfContext
    {
        public String RequestUri { get; set; }

        public String HostUri { get; set; }

        public Boolean IsIdentified { get; set; }

        public Boolean IsIdentifiedAsAdmin { get; set; }

        public String Identification { get; set; }

        public Team Team { get; set; }
    }
}