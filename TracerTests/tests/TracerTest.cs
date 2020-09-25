using System.Collections.Concurrent;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLibrary.tracer;

namespace TracerTests.tests
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
            Assert.AreEqual(2, tracerResult.Threads.ToArray()[0].ThreadMethods.Count);
        }

        [TestMethod]
        public void AmountOfTraceInnerMethods_IsRight()
        {
            TracerTestMethod();
            var tracerResult = _tracer.GetTracerResult();
            var tracerThread = tracerResult.Threads.ToArray()[0];
            var threadMethod = tracerThread.ThreadMethods.ToArray()[0];
            var internalMethods = threadMethod.InternalMethods;
            var internalMethodsCount = internalMethods.Count;
            Assert.AreEqual(1, internalMethodsCount);
            var method = internalMethods;
            Assert.AreEqual("TracerTestInnerMethod", method[0].MethodName);
            Assert.AreEqual("TracerTest", method[0].ClassName);
        }

        [TestMethod]
        public void TracerNameAndClass_IsRight()
        {
            TracerTestInnerMethod();
            var tracerResult = _tracer.GetTracerResult();
            Assert.AreEqual("TracerTestInnerMethod", tracerResult.Threads[0].ThreadMethods[0].MethodName);
            Assert.AreEqual("TracerTest", tracerResult.Threads[0].ThreadMethods[0].ClassName);
        }
    }
}