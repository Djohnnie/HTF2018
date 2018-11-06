using HTF2018.Backend.Common.Model;
using System;
using System.Collections.Generic;

namespace HTF2018.Backend.Logic.Challenges
{
    public static class Challenges
    {
        public static readonly Dictionary<Identifier, String> Titles = new Dictionary<Identifier, String>
        {
            { Identifier.Challenge01, "Identify yourself!" }
        };
        
        public static readonly Dictionary<Identifier, String> Descriptions = new Dictionary<Identifier, String>
        {
            { Identifier.Challenge01, "Our team of physicists investigated the artifact and found that it contains twenty secrets that need to be unlocked. These secrets are translated into challenges for you to work on. The first challenge is simply initializing the artifact by identifying yourself with our middleware-interface-system." }
        };
    }
}