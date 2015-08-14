using System;
using System.Diagnostics;

namespace toarray.utils
{
    public class Benchmark : IDisposable
    {
        private Stopwatch _watch;
        private string _name;

        public static Benchmark Start(string name)
        {
            return new Benchmark(name);
        }

        private Benchmark(string name)
        {
            _name = name;
            _watch = new Stopwatch();
            _watch.Start();
        }

        #region IDisposable implementation

        public void Dispose()
        {
            _watch.Stop();
            Console.WriteLine("{0} Total seconds: {1}", _name, _watch.Elapsed.TotalSeconds);
        }

        #endregion
    }
}
