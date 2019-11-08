using LageHelersonBoosterTest2019.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestProjectBooster.Service
{
    public class DataServiceTest
    {

        private readonly IDataService dataService;
        public DataServiceTest()
        {
            dataService = new DataService();
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

            //assert
            Assert.Equal(datastring,result);

        }


        [Theory]
        [InlineData("New",3 )]
        [InlineData("Zealand",7)]
        [InlineData("Milano ", 6)]
        [InlineData("Pneumonoultramicroscopicsilicovolcanoconiosis  ", 45)]
        [InlineData("Pseudopseudohypoparathyroidism", 30)]
        [InlineData(" ", 0)]
        [InlineData("",0 )]
        public void Should_Return_Word_Length(string word, int lenght)
        {
            
            //act
            var result = dataService.GetWordLength(word);

            //assert
            Assert.Equal(lenght, result);

        }

        [Fact]
        public void Should_Return_Word_And_Frequency()
        {
            //arrange  
            var text = "One Two two three threE Three";

            //act
            var result = dataService.GetWordFrequency(text);


            
            //assert
            Assert.Equal("one", result.ElementAt(0).Key.ToLower());
            Assert.Equal(1, result.ElementAt(0).Value);

            Assert.Equal("two", result.ElementAt(1).Key.ToLower());
            Assert.Equal(2, result.ElementAt(1).Value);

            Assert.Equal("three", result.ElementAt(2).Key.ToLower());
            Assert.Equal(3, result.ElementAt(2).Value);
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
}
