using System;

namespace HTF2018.Backend.DataAccess.Entities
{
    public class Challenge
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public Team Team { get; set; }

    }
}