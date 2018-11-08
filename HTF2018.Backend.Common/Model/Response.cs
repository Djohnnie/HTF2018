using System;

namespace HTF2018.Backend.Common.Model
{
    public class Response
    {
        public Identifier Identifier { get; set; }
        public Status Status { get; set; }
        public String Identification { get; set; }
        public Overview Overview { get; set; }
    }
}