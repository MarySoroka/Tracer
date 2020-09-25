using System.Threading;
using TracerLibrary;
using TracerLibrary.tracer;

namespace Tracer
{
    public class TracerExample
    {
        private readonly ITracer _tracer;
        private readonly TraceExampleInner _traceExample;

        public TracerExample(ITracer tracer, TraceExampleInner traceExample)
        {
            _tracer = tracer;
            _traceExample = traceExample;
        }

        public void ExampleMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            ExampleInnerMethod();
            _traceExample.ExampleInnerClassMethod();
            _tracer.StopTrace();
        }

        private void ExampleInnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}