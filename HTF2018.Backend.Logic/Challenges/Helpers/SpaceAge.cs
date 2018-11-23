using System;

namespace HTF2018.Backend.Logic.Challenges
{
    public class SpaceAgeHelper
    {
        #region Constants

        public const double EarthSolarYear = 31557600;
        public const double MercuryYearsPerEarthYear = 0.2408467;
        public const double VenusYearsPerEarthYear = 0.61519726;
        public const double MarsYearsPerEarthYear = 1.8808158;
        public const double JupiterYearsPerEarthYear = 11.862615;
        public const double SaturnYearsPerEarthYear = 29.447498;
        public const double UranusYearsPerEarthYear = 84.016846;
        public const double NeptuneYearsPerEarthYear = 164.79132;

        #endregion Constants

        public double Seconds { get; private set; }

        public SpaceAgeHelper(double seconds)
        {
            Seconds = seconds;
        }

        public double OnEarth()
        {
            return GetYears();
        }

        public double OnMercury()
        {
            return GetYears(MercuryYearsPerEarthYear);
        }

        public double OnVenus()
        {
            return GetYears(VenusYearsPerEarthYear);
        }

        public double OnMars()
        {
            return GetYears(MarsYearsPerEarthYear);
        }

        public double OnJupiter()
        {
            return GetYears(JupiterYearsPerEarthYear);
        }

        public double OnSaturn()
        {
            return GetYears(SaturnYearsPerEarthYear);
        }

        public double OnUranus()
        {
            return GetYears(UranusYearsPerEarthYear);
        }

        public double OnNeptune()
        {
            return GetYears(NeptuneYearsPerEarthYear);
        }

        private double GetYears(double divisor = 1.0)
        {
            return Math.Round(Seconds / EarthSolarYear / divisor, 2);
        }
    }
}