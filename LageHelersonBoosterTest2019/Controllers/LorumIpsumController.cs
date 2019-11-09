using System;
using System.Linq;
using System.Net.Mime;
using LageHelersonBoosterTest2019.Model;
using LageHelersonBoosterTest2019.Service;
using LageHelersonBoosterTest2019.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LageHelersonBoosterTest2019.Controllers
{
    //[Route("api/[controller]")]
    public class LorumIpsumController : Controller
    {

        private readonly IDataService dataService;
        private readonly ILorumIpsumDataModel lorumIpsumDataModel;

        public LorumIpsumController(ILorumIpsumDataModel lorumIpsumDataModel, IDataService dataService)
        {
            this.dataService = dataService;
            this.lorumIpsumDataModel = lorumIpsumDataModel;
        }


        /// <summary>
        /// This api gets information about the Lorum Ipsum text: 
        /// </summary>
        /// <returns>
        ///  •	Total number of characters and words. 
        ///  •	The 5 largest and 5 smallest words. 
        ///  •	10 most frequently used words.
        ///  •	List showing all the characters used in the text and the number of times they appear sorted in descending order.
        /// </returns>
        [HttpGet]
        [Route("api/GetTextInfo")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Getdata()
        {

            try
            {
                var dataStream = lorumIpsumDataModel.LoadDataLorumIpsum();
                var dataString = dataService.StreamToString(dataStream);
                var words = dataService.GetWordDetail(dataString);
                var chars = dataService.GetCharactersFrequency(dataString);

                var result = new LorumIpsumDetailsViewModel
                {
                    TotalWords = words.Sum(a => a.Frequency),
                    Totalcharacters = chars.Sum(s => s.Value),
                    FivelargestWord = words.OrderByDescending(a => a.Length).Take(5).Select(w => w.Word).ToList(),
                    FiveSmallestWord = words.OrderBy(a => a.Length).Take(5).Select(w => w.Word).ToList(),
                    TenMostFrequentlyWord = words.OrderByDescending(a => a.Frequency).Take(10).Select(w => w.Word).ToList(),
                    Characters = chars.AsQueryable().Select(c => new Entity.Character { Char = c.Key, Frequency = c.Value }).OrderByDescending(o => o.Frequency).ToList()
                };

                return Ok(value: Json(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
