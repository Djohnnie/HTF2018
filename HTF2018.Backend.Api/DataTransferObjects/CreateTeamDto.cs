using System;

namespace HTF2018.Backend.Api.DataTransferObjects
{
    public class CreateTeamDto
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Password { get; set; }
    }
}