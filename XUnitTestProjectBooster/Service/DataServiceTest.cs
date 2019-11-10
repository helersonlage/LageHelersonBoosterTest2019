using LageHelersonBoosterTest2019.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

    public class DataServiceTest
    {
       private readonly IDataService dataService;

        public DataServiceTest()
        {
            //Dependency Injection
            var services = new ServiceCollection();
            services.AddTransient<IDataService, DataService>();
            var serviceProvider = services.BuildServiceProvider();
            dataService = serviceProvider.GetService<IDataService>();
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore")]
        [InlineData("Ut enim ad minim veniam")]
        [InlineData("Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium")]
        public void Should_Convert_StreamReader_To_String(string datastring)
        {
            //arrange                    
            StreamReader streamReader = GenerateStreamReaderFromString(datastring);

            //act
            var result = dataService.StreamToString(streamReader);

            result = result;
            //assert
            Assert.Equal(datastring, result);

        }


        [Theory]
        [InlineData("New", 3)]
        [InlineData("Zealand", 7)]
        [InlineData("Milano ", 6)]
        [InlineData(" Pneumonoultramicroscopicsilicovolcanoconiosis  ", 45)]
        [InlineData("Pseudopseudohypoparathyroidism", 30)]
        [InlineData(" ", 0)]
        [InlineData("", 0)]
        public void Should_Return_Word_Length(string word, int lenght)
        {
            //act
            var result = dataService.GetWordLength(word);

            //assert
            Assert.Equal(lenght, result);
        }

        [Fact]
        public void Should_Return_Words_And_Frequencies()
        {
            //arrange  
            var text = "One Two two three threE Three";

            //act
            var result = dataService.GetWordsFrequency(text);

            //assert
            Assert.Equal("one", result.ElementAt(0).Key.ToLower());
            Assert.Equal(1, result.ElementAt(0).Value);

            Assert.Equal("two", result.ElementAt(1).Key.ToLower());
            Assert.Equal(2, result.ElementAt(1).Value);

            Assert.Equal("three", result.ElementAt(2).Key.ToLower());
            Assert.Equal(3, result.ElementAt(2).Value);
        }


        [Fact]
        public void Should_Return_characters_And_Frequencies()
        {
            // Expected result
            var expectedResult = new Dictionary<string, int>()
            {
                {"g",2},
                {"e",4},
                {"k",2},
                {"s",2},
                {"f",1},
                {"o",1},
                {"r",1},
            };

            //arrange  
            var text = " geeks for GEeKs ";

            //act         
            var result = dataService.GetCharactersFrequency(text);

            //assert    
            Assert.Equal(7, result.Count());
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Load_Word_Frenquency_Lenght()
        {
            //Arrange
            var text = "One Two two three threE Three four four four four";

            //act
            var result = dataService.GetWordDetail(text);

            //assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);

            Assert.Equal("One", result[0].Word);
            Assert.Equal(3, result[0].Length);
            Assert.Equal(1, result[0].Frequency);

            Assert.Equal("Two", result[1].Word);
            Assert.Equal(3, result[1].Length);
            Assert.Equal(2, result[1].Frequency);

            Assert.Equal("three", result[2].Word);
            Assert.Equal(5, result[2].Length);
            Assert.Equal(3, result[2].Frequency);

            Assert.Equal("four", result[3].Word);
            Assert.Equal(4, result[3].Length);
            Assert.Equal(4, result[3].Frequency);

        }


        #region private methods
        private static StreamReader GenerateStreamReaderFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return new StreamReader(stream);
        }
        #endregion
    }

