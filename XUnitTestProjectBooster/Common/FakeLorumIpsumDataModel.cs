using LageHelersonBoosterTest2019.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestProjectBooster.Controler
{
    internal class FakeLorumIpsumDataModel : ILorumIpsumDataModel
    {
        public FakeLorumIpsumDataModel(){ }

        private const string testdata = "Lorem ipsum dolor sit amet dolore enim elit turpis sit odio elit facilisi ante tortor velit ante ut urna amet ut iaculis";         

        /// <summary>
        /// Fake Stream Reader data 
        /// </summary>
        /// <returns></returns>
        public StreamReader LoadDataLorumIpsum()
        {            
            StreamReader reader = Common.Common.GenerateStreamReaderFromString(testdata);
            return reader;
        }

    }
}
