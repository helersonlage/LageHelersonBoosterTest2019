using LageHelersonBoosterTest2019.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        /// <summary>
        ///  Get a word list 
        /// </summary>
        /// <param name="text"></param>
        /// <returns> word, frequency, length</returns>
        public List<WordDetail> GetWordDetail(string text)
        {
            WordDetail word;
            //Length and word Frequencies
            var words = new List<WordDetail>();             
            var wordFrequencies = GetWordsFrequency(text);

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
        /// Get word frequency
        /// </summary>
        /// <param name="text">text to analise</param>
        /// <returns>Dictionary<string, int> word and frequency</returns>
        public Dictionary<string, int> GetWordsFrequency(string text)
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
        ///  Get characters frequency 
        /// </summary>       
        public Dictionary<string, int> GetCharactersFrequency(string text)
        {
            var dicLetters = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

            text = text.Replace(System.Environment.NewLine, string.Empty).Replace(" ", string.Empty);

            foreach (char c in text)
            {
                int currentCount = 0;

                dicLetters.TryGetValue(c.ToString().ToLower(), out currentCount);

                currentCount++;
                dicLetters[c.ToString()] = currentCount;

            }

            return dicLetters;
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

