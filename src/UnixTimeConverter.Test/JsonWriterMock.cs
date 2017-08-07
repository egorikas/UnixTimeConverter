using System;
using Newtonsoft.Json;

namespace UnixTimeConverter.Test
{
    class JsonWriterMock : JsonWriter
    {
        public long FetchedData { get; set; }

        public override void WriteValue(long value)
        {
            FetchedData = value;
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
