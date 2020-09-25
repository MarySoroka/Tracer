using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using Tracer.serializer;
using TracerLibrary.tracer;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace Tracer
{
    internal static class TracerApp
    {
        private const string JsonFile = "result.json";
        private const string XmlFile = "result.xml";

        private static readonly ISerializer JsonSerializer =
            new JsonSerializer(new DataContractJsonSerializer(typeof(TracerResult)));

        private static readonly ISerializer XmlSerializer = new serializer.XmlSerializer(
            new XmlSerializer(typeof(TracerResult)), new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            });

        private static void Main()
        {
            var tracerResult = new TracerResult(new ConcurrentDictionary<int, TracerThread>());
            var tracer = new TracerLibrary.tracer.Tracer(tracerResult);
            var traceExampleInner = new TraceExampleInner(tracer);
            var example = new TracerExample(tracer, traceExampleInner);
            example.ExampleMethod();
            var traceResult = tracer.GetTracerResult();
            using var outputStream = Console.OpenStandardOutput();
            using var xmlFileStream = new FileStream(XmlFile,
                FileMode.Create, FileAccess.Write);
            using var jsonFileStream = new FileStream(JsonFile,
                FileMode.Create, FileAccess.Write);
            XmlSerializer.Serialize(traceResult, outputStream);
            Console.Write("\n\n");
            JsonSerializer.Serialize(traceResult, outputStream);
            XmlSerializer.Serialize(traceResult, xmlFileStream);
            JsonSerializer.Serialize(traceResult, jsonFileStream);
        }
    }
}