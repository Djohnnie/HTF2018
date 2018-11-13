using System;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.DataAccess.Entities
{
    public class History
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public Status Status { get; set; }
    }
}