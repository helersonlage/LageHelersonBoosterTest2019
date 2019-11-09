using LageHelersonBoosterTest2019.Entity;
using System.Collections.Generic;
using System.IO;

namespace LageHelersonBoosterTest2019.Service
{
    public interface IDataService
    {
        int GetWordLength(string text);
        string StreamToString(StreamReader dataStream);
        Dictionary<string, int> GetWordsFrequency(string text);
        Dictionary<string, int> GetCharactersFrequency(string text);        
        List<WordDetail> GetWordDetail(string text);
    }
}