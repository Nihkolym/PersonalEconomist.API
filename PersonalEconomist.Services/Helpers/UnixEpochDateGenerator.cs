using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Services.Helpers
{
    public static class UnixEpochDateGenerator
    {
        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}
