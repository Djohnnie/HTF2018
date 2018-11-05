using System;

namespace HTF2018.Backend.DataAccess.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }
        public String Name { get; set; }
        public String PasswordHash { get; set; }
    }
}