using System.IO;
using TracerLibrary.tracer;

namespace Tracer.serializer
{
    public class XmlSerializer : ISerializer
    {
        public string Serialize(TracerResult tracerResult)
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(tracerResult.GetType());
            using var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, tracerResult);
            return textWriter.ToString();
        }
    }
}