using System.IO;
using TracerLibrary.tracer;

namespace Tracer.serializer
{
    public interface ISerializer
    {
        void Serialize(TracerResult traceResult, Stream fileOutStream);
    }
}
