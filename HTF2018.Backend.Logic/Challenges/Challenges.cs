using HTF2018.Backend.Common.Model;
using System.Collections.Generic;

namespace HTF2018.Backend.Logic.Challenges
{
    public static class Challenges
    {
        public static readonly Dictionary<Identifier, string> Titles = new Dictionary<Identifier, string>
        {
            { Identifier.Challenge01, "Identify yourself!" },
            { Identifier.Challenge02, "A simple challenge to get started!" },
            { Identifier.Challenge03, "Cipherific!" },
            { Identifier.Challenge04, "Let's get it more difficult!" },
            { Identifier.Challenge05, "Age, Time, ..?" },
            { Identifier.Challenge06, "Monday, Tuesday,.." },
            { Identifier.Challenge07, "Do you care about exceptions?" },
            { Identifier.Challenge08, "01001000 01100101 01101100 01101100 01101111" },
            { Identifier.Challenge09, "Everyone can do some basic formula's" },
            { Identifier.Challenge10, "What is this image?" },
            { Identifier.Challenge11, "Ciphers, again?" },
            { Identifier.Challenge12, "Why would they send squares?" },
            { Identifier.Challenge13, "Geo(de)coding" },
            { Identifier.Challenge14, "" },
            { Identifier.Challenge15, "Math, Math, You've done that" },
            { Identifier.Challenge16, "Roman Empire" },
            { Identifier.Challenge17, "" },
            { Identifier.Challenge18, "Kanto, Johto, Hoenn,..;" },
            { Identifier.Challenge19, "A maze? Really?" },
            { Identifier.Challenge20, "Final piece.." }
        };

        public static readonly Dictionary<Identifier, string> Descriptions = new Dictionary<Identifier, string>
        {
            { Identifier.Challenge01, "Our team of physicists investigated the artifact and found that it contains twenty secrets that need to be unlocked. These secrets are translated into challenges for you to work on. The first challenge is simply initializing the artifact by identifying yourself with our engineer-friendly-middleware-interface-system." },
            { Identifier.Challenge02, "It seems our basic intelligence is being tested and a simple sum of random integers is what is expected." },
            { Identifier.Challenge03, "The artifact is sending us messages. See if you can decode them!" },
            { Identifier.Challenge04, "It might have been a long time, but the artifact seems to have all the knowledge. Please answer it!" },
            { Identifier.Challenge05, "The artifact is trying sending us some sort of time, see if you can convert it on different planets!" },
            { Identifier.Challenge06, "They are getting in our head. Are they challenging our own calendar?" },
            { Identifier.Challenge07, "It seems they are not always giving what we expect." },
            { Identifier.Challenge08, "01011001 01100101 01100001 01101000 00100000 01111001 01101111 01110101 00100000 01101011 01101110 01101111 01110111 00100000 01110111 01101000 01100001 01110100 00100000 01110100 01101111 00100000 01100100 01101111" },
            { Identifier.Challenge09, "They are sending a lot of stuff. Can you make the full formula and process it?" },
            { Identifier.Challenge10, "The artifact is trying to communicate using modern methods and generates weird black and white images. What do these images represent and what is the underlying message?" },
            { Identifier.Challenge11, "They are sending messages again. I can't seem to decode them, can you try again?" },
            { Identifier.Challenge12, "Try to count the squares, beware it can get more difficult!" },
            { Identifier.Challenge13, "Are these just numbers or do they have a different meaning.." },
            { Identifier.Challenge14, "" },
            { Identifier.Challenge15, "Some new mathematical formula u have to solve" },
            { Identifier.Challenge16, "Oh, the artifact seems to unlock. This challenge is a breeze!" },
            { Identifier.Challenge17, "" },
            { Identifier.Challenge18, "That rings a bell, Pokemon?" },
            { Identifier.Challenge19, "We are being compared with test subjects like mice and are confronted with a maze. Solve the maze and find the shortest route from start to finish." },
            { Identifier.Challenge20, "The final piece to the puzzle, decode the image and we can see what truly hides in the artifact." }
        };
    }
}