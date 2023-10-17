using Serilog;
using System;
using System.Diagnostics;

namespace APPLICATION.Shared.Helpers
{
    public class Metrics : IDisposable
    {
        private readonly string _methodName;
        private readonly Stopwatch _stopwatch = new();

        public Metrics(object request)
        {
            _methodName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.FullName;
            _stopwatch.Start();
            Request = request;
        }

        public object Request { get; }

        public void Dispose()
        {
            _stopwatch.Stop();
            Log.Information("{@methodBase} {@Request} in {Elapsed:000} ms.", _methodName, Request, _stopwatch.ElapsedMilliseconds);
        }
    }
}