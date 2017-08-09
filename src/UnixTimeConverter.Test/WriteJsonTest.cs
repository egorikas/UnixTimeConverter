using System;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace UnixTimeConverter.Test
{
    public class WriteJsonTest
    {
        [Fact]
        public void WriteJson_WrongInput_ExceptionThrown()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();

            //Act
            var ex = Assert.Throws<ArgumentException>(() => unixTimeConverter.WriteJson(null, string.Empty, null));

            //Assert
            Assert.Equal("Expect DateTime as input parameter", ex.Message);
        }

        [Fact]
        public void WriteJson_LessThan1970_SuccessResult()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonWriterMock = new Mock<JsonWriter>();

            //Act
            unixTimeConverter.WriteJson(jsonWriterMock.Object, new DateTime(1950, 1, 1, 0, 0, 0, DateTimeKind.Utc), null);

            //Assert
            jsonWriterMock.Verify(x => x.WriteValue(-631152000L), Times.Once);
        }

        [Fact]
        public void WriteJson_MoreThan1970_SuccessResult()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonWriterMock = new Mock<JsonWriter>();

            //Act
            unixTimeConverter.WriteJson(jsonWriterMock.Object, new DateTime(1971, 1, 1, 0, 0, 0, DateTimeKind.Utc), null);

            //Assert
            jsonWriterMock.Verify(x => x.WriteValue(31536000L), Times.Once);
        }

        [Fact]
        public void WriteJson_FullDate_SuccessResult()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonWriterMock = new Mock<JsonWriter>();

            //Act
            unixTimeConverter.WriteJson(jsonWriterMock.Object, new DateTime(2017, 08, 04, 16, 2, 2, DateTimeKind.Utc), null);

            //Assert
            jsonWriterMock.Verify(x => x.WriteValue(1501862522L), Times.Once);
        }
    }
}