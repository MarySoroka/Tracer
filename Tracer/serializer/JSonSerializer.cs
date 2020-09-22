using System.Text.Json;
using TracerLibrary.tracer;

namespace Tracer.serializer
{
    public class JSonSerializer : ISerializer
    {
        public string Serialize(TracerResult tracerResult)
        {
            return JsonSerializer.Serialize(tracerResult);
        }
    }
}