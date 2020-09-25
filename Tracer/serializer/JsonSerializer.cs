using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using TracerLibrary.tracer;
namespace Tracer.serializer
{
    public class JsonSerializer : ISerializer
    {
        private readonly DataContractJsonSerializer _jsonSerializer;

        public JsonSerializer(DataContractJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public void Serialize(TracerResult traceResult, Stream fileOutStream)
        {
            using var writer = JsonReaderWriterFactory.CreateJsonWriter(fileOutStream, Encoding.UTF8, true, true);
            _jsonSerializer.WriteObject(writer, traceResult);
        }

       
    }
}
