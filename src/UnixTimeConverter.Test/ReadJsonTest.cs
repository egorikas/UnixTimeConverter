using System;
using Moq;
using Newtonsoft.Json;
using UnixTimeConverter.Test.Helpers;
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
            Assert.Equal("Unexpected token. Integer or String was expected, got Boolean", ex.Message);
        }

        [Fact]
        public void WriteJson_InvalidString_ExceptionThrown()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonReaderMock = new Mock<JsonReader>();
            jsonReaderMock.SetupGet(x => x.TokenType).Returns(JsonToken.String);
            jsonReaderMock.SetupGet(x => x.Value).Returns("wrongInput");

            //Act
            var ex = Assert.Throws<ArgumentException>(
                () => unixTimeConverter.ReadJson(jsonReaderMock.Object, null, false, null));

            //Assert
            Assert.Equal("wrongInput isn't a number", ex.Message);
        }

        [Theory]
        [ClassData(typeof(DataHelper))]
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
    }
}