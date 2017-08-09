using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnixTimeConverter.Test.Helpers
{
    public class DataHelper : IEnumerable<object[]>
    {
        private readonly IEnumerable<object[]> _data = new[]
        {
            new object[] {JsonToken.Integer, 1355314332, new DateTime(2012, 12, 12, 12, 12, 12, DateTimeKind.Utc)},
            new object[] {JsonToken.String, "1355314332", new DateTime(2012, 12, 12, 12, 12, 12, DateTimeKind.Utc)},
            new object[] {JsonToken.Integer, -631108068, new DateTime(1950, 1, 1, 12, 12, 12, DateTimeKind.Utc)}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
