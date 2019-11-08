using LageHelersonBoosterTest2019.Entity;
using System.Collections.Generic;
using System.IO;

namespace LageHelersonBoosterTest2019.Service
{
    public interface IDataService
    {
        string StreamToString(StreamReader dataStream);
        Dictionary<string, int> GetWordFrequency(string text);
        int GetWordLength(string text);
        List<WordDetail> GetWordDetail(string text);
    }
}