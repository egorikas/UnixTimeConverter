using System;
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
            Assert.Equal(ex.Message, "Expect DateTime as input parameter");
        }

        [Fact]
        public void WriteJson_LessThan1970_SuccessResult()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonWriterMock = new JsonWriterMock();

            //Act
            unixTimeConverter.WriteJson(jsonWriterMock, new DateTime(1950, 1, 1, 0, 0, 0, DateTimeKind.Utc), null);

            //Assert
            Assert.Equal(-631152000, jsonWriterMock.FetchedData);
        }

        [Fact]
        public void WriteJson_MoreThan1970_SuccessResult()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonWriterMock = new JsonWriterMock();

            //Act
            unixTimeConverter.WriteJson(jsonWriterMock, new DateTime(1971, 1, 1, 0, 0, 0, DateTimeKind.Utc), null);

            //Assert
            Assert.Equal(31536000, jsonWriterMock.FetchedData);
        }

        [Fact]
        public void WriteJson_FullDate_SuccessResult()
        {
            //Arrange
            var unixTimeConverter = new UnixTimeConverter();
            var jsonWriterMock = new JsonWriterMock();

            //Act
            unixTimeConverter.WriteJson(jsonWriterMock, new DateTime(2017, 08, 04, 16, 2, 2, DateTimeKind.Utc), null);

            //Assert
            Assert.Equal(1501862522, jsonWriterMock.FetchedData);
        }
    }
}