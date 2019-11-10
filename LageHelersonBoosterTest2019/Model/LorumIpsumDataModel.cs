using DevTest;
using System.IO;

namespace LageHelersonBoosterTest2019.Model
{
    public class LorumIpsumDataModel : ILorumIpsumDataModel
    {
        public LorumIpsumDataModel(){}

        /// <summary>
        /// Read data from Dll provided 
        /// </summary>
        /// <returns></returns>
        public StreamReader LoadDataLorumIpsum()
        {
            var Data = new LorumIpsumStream();
            StreamReader reader = new StreamReader(Data);
            return reader;
        }

    }
}
