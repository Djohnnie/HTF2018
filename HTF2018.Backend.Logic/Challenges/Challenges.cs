using HTF2018.Backend.Common.Model;
using System;
using System.Collections.Generic;

namespace HTF2018.Backend.Logic.Challenges
{
    public static class Challenges
    {
        public static readonly Dictionary<Identifier, String> Titles = new Dictionary<Identifier, String>
        {
            { Identifier.Challenge01, "Identify yourself!" },
            { Identifier.Challenge02, "A simple challenge to get started!" },
            { Identifier.Challenge03, "" },
            { Identifier.Challenge04, "" },
            { Identifier.Challenge05, "" },
            { Identifier.Challenge06, "" },
            { Identifier.Challenge07, "" },
            { Identifier.Challenge08, "" },
            { Identifier.Challenge09, "" },
            { Identifier.Challenge10, "What is this image?" },
            { Identifier.Challenge11, "" },
            { Identifier.Challenge12, "" },
            { Identifier.Challenge13, "" },
            { Identifier.Challenge14, "" },
            { Identifier.Challenge15, "" },
            { Identifier.Challenge16, "" },
            { Identifier.Challenge17, "" },
            { Identifier.Challenge18, "" },
            { Identifier.Challenge19, "A maze? Really?" },
            { Identifier.Challenge20, "" }
        };

        public static readonly Dictionary<Identifier, String> Descriptions = new Dictionary<Identifier, String>
        {
            { Identifier.Challenge01, "Our team of physicists investigated the artifact and found that it contains twenty secrets that need to be unlocked. These secrets are translated into challenges for you to work on. The first challenge is simply initializing the artifact by identifying yourself with our engineer-friendly-middleware-interface-system." },
            { Identifier.Challenge02, "It seems our basic intelligence is being tested and a simple sum of random integers is what is expected." },
            { Identifier.Challenge03, "" },
            { Identifier.Challenge04, "" },
            { Identifier.Challenge05, "" },
            { Identifier.Challenge06, "" },
            { Identifier.Challenge07, "" },
            { Identifier.Challenge08, "" },
            { Identifier.Challenge09, "" },
            { Identifier.Challenge10, "The artifact is trying to communicate using modern methods and generates weird black and white images. What do these images represent and what is the underlying message?" },
            { Identifier.Challenge11, "" },
            { Identifier.Challenge12, "" },
            { Identifier.Challenge13, "" },
            { Identifier.Challenge14, "" },
            { Identifier.Challenge15, "" },
            { Identifier.Challenge16, "" },
            { Identifier.Challenge17, "" },
            { Identifier.Challenge18, "" },
            { Identifier.Challenge19, "We are being compared with test subjects like mice and are confronted with a maze. Solve the maze and find the shortest route from start to finish." },
            { Identifier.Challenge20, "" }
        };
    }
}