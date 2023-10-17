using System;

namespace APPLICATION.Shared.Helpers
{
    public static class Datetime
    {
        public static DateTime Minimal() => new(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}