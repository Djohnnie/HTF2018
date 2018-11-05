using System;

namespace HTF2018.Backend.Api.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Password { get; set; }
    }
}