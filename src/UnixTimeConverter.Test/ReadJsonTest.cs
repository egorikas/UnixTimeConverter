using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace UnixTimeConverter.Test
{
    public class ReadJsonTest
    {
        [Fact]
        public void WriteJson_InvalidTokenType_ExceptionThrown()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonReaderMock = new Mock<JsonReader>();
            jsonReaderMock.SetupGet(x => x.TokenType).Returns(JsonToken.Boolean);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => unixTimeConverter.ReadJson(jsonReaderMock.Object, null, false, null));

            //Assert
            Assert.Equal("Unexpected token parsing date. Integer or String was expected, got Boolean", ex.Message);
        }

        [Theory]
        [InlineData(JsonToken.Integer, 1355314332)]
        [InlineData(JsonToken.String, "1355314332")]
        public void WriteJson_ValidPositiveInput_Success(JsonToken jsonTokenType, object value)
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonReaderMock = new Mock<JsonReader>();
            jsonReaderMock.SetupGet(x => x.TokenType).Returns(jsonTokenType);
            jsonReaderMock.SetupGet(x => x.Value).Returns(value);

            //Act
            var result = unixTimeConverter.ReadJson(jsonReaderMock.Object, null, false, null);

            //Assert
            Assert.Equal(new DateTime(2012,12,12,12,12,12, DateTimeKind.Utc), result);
        }
    }
}
