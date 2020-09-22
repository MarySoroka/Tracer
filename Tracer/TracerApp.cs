using System;
using System.Collections.Concurrent;
using System.IO;
using Tracer.serializer;
using TracerLibrary;
using TracerLibrary.tracer;

namespace Tracer
{
    internal static class TracerApp
    {
        private static readonly ISerializer JsonSerializer = new JSonSerializer();
        private static readonly ISerializer XmlSerializer = new XmlSerializer();
        private const string JsonFile = "result.json";
        private const string XmlFile = "result.xml";


        private static void Main(string[] args)
        {
            var tracerResult = new TracerResult(new ConcurrentDictionary<int, TracerThread>());
            var tracer = new TracerLibrary.tracer.Tracer(tracerResult);
            var traceExampleInner = new TraceExampleInner(tracer);
            var example = new TracerExample(tracer,traceExampleInner);
            example.ExampleMethod();
            var result = tracer.GetTracerResult();
            var jsonResult = JsonSerializer.Serialize(result);
            var xmlResult = XmlSerializer.Serialize(result);
            Console.Write(jsonResult);
            Console.Write(xmlResult);
            
            File.WriteAllText(JsonFile, jsonResult);
            File.WriteAllText(XmlFile, jsonResult);
        }
    }
}