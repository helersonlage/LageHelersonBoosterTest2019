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
        public List<string> FivelargestWords { get; set; }
        public List<string> FiveSmallestWords { get; set; }
        public List<string> TenMostFrequentlyWords { get; set; }
        public List<Character> Characters { get; set; }

    }
}
