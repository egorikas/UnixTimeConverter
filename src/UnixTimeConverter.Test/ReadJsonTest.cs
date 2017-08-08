using System;
using System.Collections.Generic;
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
            var ex = Assert.Throws<ArgumentException>(
                () => unixTimeConverter.ReadJson(jsonReaderMock.Object, null, false, null));

            //Assert
            Assert.Equal("Unexpected token parsing date. Integer or String was expected, got Boolean", ex.Message);
        }

        [Theory]
        [MemberData(nameof(PositiveInputData))]
        public void WriteJson_ValidPositiveInput_Success(JsonToken jsonTokenType, object input, DateTime expectedResult)
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonReaderMock = new Mock<JsonReader>();
            jsonReaderMock.SetupGet(x => x.TokenType).Returns(jsonTokenType);
            jsonReaderMock.SetupGet(x => x.Value).Returns(input);

            //Act
            var result = unixTimeConverter.ReadJson(jsonReaderMock.Object, null, false, null);

            //Assert
            Assert.Equal(expectedResult, result);
        }


        public static IEnumerable<object[]> PositiveInputData => new[]
        {
            new object[] {JsonToken.Integer, 1355314332, new DateTime(2012, 12, 12, 12, 12, 12, DateTimeKind.Utc)},
            new object[] {JsonToken.String, "1355314332", new DateTime(2012, 12, 12, 12, 12, 12, DateTimeKind.Utc)},
            new object[] {JsonToken.Integer, -631108068, new DateTime(1950, 1, 1, 12, 12, 12, DateTimeKind.Utc)}
        };
    }
}