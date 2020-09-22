using TracerLibrary.tracer;

namespace Tracer.serializer
{
    public interface ISerializer
    {
        string Serialize(TracerResult tracerResult);
    }
}