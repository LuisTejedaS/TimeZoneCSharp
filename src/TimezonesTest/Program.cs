using System;
using TimeZoneConverter;

namespace TimezonesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Central Standard Time");
            TimeZoneInfo tzi2 = TZConvert.GetTimeZoneInfo("America/New_York");
            Console.WriteLine(tzi);
            Console.WriteLine(tzi2);
        }
    }
}
