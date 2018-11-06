using HTF2018.Backend.Common.Model;
using System;

namespace HTF2018.Backend.DataAccess.Entities
{
    public class Challenge
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public Identifier Identifier { get; set; }
        public Status Status { get; set; }
        public DateTime? SolvedOn { get; set; }
        public Team Team { get; set; }
        public String Question { get; set; }
        public String Answer { get; set; }
    }
}