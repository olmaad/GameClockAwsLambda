using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameClockTestLambda
{
    public static class Utils
    {
        public static string SelectRandom(IEnumerable<string> from)
        {
            return from.ElementAt(new Random(DateTime.Now.Millisecond).Next(from.Count()));
        }

        public static string SelectRandomExcept(IEnumerable<string> from, string exceptValue)
        {
            var except = from.Except(new List<string>() { exceptValue });

            return SelectRandom(except);
        }

        public static string FormatTime(TimeSpan time)
        {
            if (time.TotalSeconds < 60)
            {
                return time.ToString("s' seconds'");
            }
            else if (time.TotalMinutes < 60)
            {
                return time.ToString("m' minutes 's' seconds'");
            }

            return time.ToString("m' minutes 's' seconds'");
        }
    }
}
