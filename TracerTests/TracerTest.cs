using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLibrary;

namespace TracerTests
{
    [TestClass]
    public class TracerTest
    {
        private readonly ITracer _tracer = new Tracer(new TracerResult(new ConcurrentDictionary<int, TracerThread>()));

        private void TracerMethodTestOfTheSameLevel()
        {
            TracerTestMethod();
            TracerTestMethod();
        }

        private void TracerTestMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            TracerTestInnerMethod();
            _tracer.StopTrace();
            
        }

        private void TracerTestInnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
            
        }
        [TestMethod]
        public void AmountOfTraceMethods_IsRight()
        {
            TracerMethodTestOfTheSameLevel();
            var tracerResult = _tracer.GetTracerResult();
            Assert.AreEqual(2, tracerResult.ConcurrentThead.Values.ToArray()[0].ThreadMethods.Count);
        }
        [TestMethod]
        public void TracerTimeExecute_IsRight()
        {
            TracerTestMethod();
            var tracerResult = _tracer.GetTracerResult();
            Assert.AreEqual("200", tracerResult.ConcurrentThead.ToArray()[0].Value.ThreadTime);
        }
        [TestMethod]
        public void AmountOfTracerThreads_IsRight()
        {
            _tracer.StartTrace();
            for (var i = 0; i < 6; ++i)
            {
                var thread = new Thread(TracerTestMethod);
                thread.Start();
            }
            _tracer.StopTrace();
            var actualCountOfThreads = _tracer.GetTracerResult().ConcurrentThead.Count;
            Assert.AreEqual(6, actualCountOfThreads);
        }

    }
}