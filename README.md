# UnixTimeConverter
[![Build status](https://ci.appveyor.com/api/projects/status/kgcd86oe1a64451a?svg=true)](https://ci.appveyor.com/project/EgorGrishechko/unixtimeconverter)
[![NuGet](https://img.shields.io/nuget/v/UnixTimeConverter.svg)](https://www.nuget.org/packages/UnixTimeConverter)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Unix Time Stamp to .NET DateTime converter for Json.NET.

## Installation

`Install-Package UnixTimeConverter`

## Usage

```csharp
namespace UsageExample
{
    public class ApiData
    {
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime Date { get; set; }
    }

    public class ConverterTest
    {
        [Fact]
        public void HappyPath()
        {
            //Arrange
            var apiJson = "{ Date : 1321009871 }";

            //Act
            var result = JsonConvert.DeserializeObject<ApiData>(apiJson);

            //Assert
            Assert.Equal(new DateTime(2011, 11, 11, 11, 11, 11, DateTimeKind.Utc), result);
        }
    }
}
```

## Build

Install [.NET Core SDK](https://www.microsoft.com/net/download/core "official site")

Open `src` folder in the command prompt.
Then 
```
    dotnet restore
    dotnet build
```
## Tests
Open `src\JsonNetConverters.Test` folder in the command prompt.
Then
```
    dotnet test
```

## .NET Standart compatibility
Library was created with supporting for **.NET Standart 1.4**

## Contributing
Don't be shy to ask a question or offer something :)