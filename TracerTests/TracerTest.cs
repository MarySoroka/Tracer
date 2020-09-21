using System.Collections.Concurrent;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLibrary;

namespace TracerTests
{
    [TestClass]
    public class TracerTest
    {
        private ITracer _tracer = new Tracer(new TracerResult(new ConcurrentDictionary<int, TracerThread>()));
        public void TracerTestMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
            
        }
        public void TracerTestInnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(300);
            _tracer.StopTrace();
            
        }
        [TestMethod]
        public void IsTracerRight()
        {
            TracerTestMethod();
            var tracerResult = _tracer.GetTracerResult();
            Assert.AreEqual(1, tracerResult.ConcurrentThead.Count);
        }
    }
}