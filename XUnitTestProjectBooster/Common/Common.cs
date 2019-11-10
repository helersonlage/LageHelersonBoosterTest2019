using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestProjectBooster.Common
{
    internal class Common
    {
        
        internal static StreamReader GenerateStreamReaderFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return new StreamReader(stream);
        }
        
    }
}
