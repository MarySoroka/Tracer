using System.Threading;
using TracerLibrary;
using TracerLibrary.tracer;

namespace Tracer
{
    public class TraceExampleInner
    {
        private readonly ITracer _tracer;

        public TraceExampleInner(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void ExampleInnerClassMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}