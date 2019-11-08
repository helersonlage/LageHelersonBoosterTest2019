using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTest;
using System.Text;
using LageHelersonBoosterTest2019.Entity;
using System.Text.RegularExpressions;

namespace LageHelersonBoosterTest2019.Service
{
    public class DataService : IDataService
    {

        /// <summary>
        /// Converter Stream Reader to String
        /// </summary>
        /// <param name="dataStream">Stream Reader</param>
        /// <returns><see cref="String"/></returns>
        public string StreamToString(StreamReader dataStream)
        {
            if (dataStream == null)
            {
                throw new ArgumentNullException(nameof(dataStream));
            }

            char[] result;
            StringBuilder builder = new StringBuilder();

            result = new char[dataStream.BaseStream.Length];
            dataStream.Read(result, 0, (int)dataStream.BaseStream.Length);


            foreach (char c in result)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }


        public List<WordDetail> GetWordDetail(string text)
        {
            WordDetail word;
            //Length word Frequencies
            var words = new List<WordDetail>();             
            var wordFrequencies = GetWordFrequency(text);

            foreach (var item in wordFrequencies)
            {
                word = new WordDetail();
                word.Word = item.Key;
                word.Frequency = item.Value;
                word.Length = GetWordLength(item.Key);

                // Add item on List
                words.Add(word);
            }
            return words;
        }


        /// <summary>
        /// Create a word and frequency dictionary
        /// </summary>
        /// <param name="text">text to analise</param>
        /// <returns>Dictionary<string, int> word and frequency</returns>
        public Dictionary<string, int> GetWordFrequency(string text)
        {
            var dictWords = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            var wordPattern = new Regex(@"\w+");

            foreach (Match match in wordPattern.Matches(text))
            {
                int currentCount = 0;
                dictWords.TryGetValue(match.Value.ToLower(), out currentCount);

                currentCount++;
                dictWords[match.Value] = currentCount;
            }
            return dictWords;
        }

        /// <summary>
        /// Get word length
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>word length</returns>
        public int GetWordLength(string text)
        {
            text = string.Join(" ", Regex.Split(text, @"(?:\r\n|\n|\r)"));
            return text.Trim().Length;
        }
    }

}

