using DevTest;
using System.IO;

namespace LageHelersonBoosterTest2019.Model
{
    public class LorumIpsumDataModel : ILorumIpsumDataModel
    {
        public LorumIpsumDataModel()
        {

        }

        public StreamReader LoadDataLorumIpsum()
        {
            var Data = new LorumIpsumStream();
            StreamReader reader = new StreamReader(Data);
            return reader;
        }


    }
}
