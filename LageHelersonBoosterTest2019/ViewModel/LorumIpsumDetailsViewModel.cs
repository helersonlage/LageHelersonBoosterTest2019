using LageHelersonBoosterTest2019.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LageHelersonBoosterTest2019.ViewModel
{
    public class LorumIpsumDetailsViewModel
    {
       
        public int TotalWords { get; set; }
        public int Totalcharacters { get; set; }        
        public List<string> FivelargestWord { get; set; }
        public List<string> FiveSmallestWord { get; set; }
        public List<string> TenMostFrequentlyWord { get; set; }
        public List<Character> Characters { get; set; }

    }
}
