using System.IO;
using System.Xml;
using TracerLibrary.tracer;

namespace Tracer.serializer
{
    public class XmlSerializer : ISerializer
    {    
        private readonly System.Xml.Serialization.XmlSerializer _xmlSerializer;
        private readonly XmlWriterSettings _xmlWriterSettings;

        public XmlSerializer(System.Xml.Serialization.XmlSerializer xmlSerializer, XmlWriterSettings xmlWriterSettings)
        {
            _xmlSerializer = xmlSerializer;
            _xmlWriterSettings = xmlWriterSettings;
        }

        public void Serialize(TracerResult traceResult, Stream fileOutStream)
        {
            using var writer = XmlWriter.Create(fileOutStream, _xmlWriterSettings);
            _xmlSerializer.Serialize(writer, traceResult);
        }

        
    }
}
