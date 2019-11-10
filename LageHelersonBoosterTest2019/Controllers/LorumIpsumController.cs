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
        ///  Get Lorum Ipsum text Info: 
        /// </summary>
        /// <param name="WhiteSpaceIsChar">Considers white space as character.</param>
        /// <returns>
        ///  •	Total number of characters and words. 
        ///  •	The 5 largest and 5 smallest words. 
        ///  •	10 most frequently used words.
        ///  •	List showing all the characters used in the text and the number of times they appear sorted in descending order.
        /// </returns> 
        /// <response code="200">Successfully returns text info</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpGet]
        [Route("api/GetTextInfo")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Getdata(bool WhiteSpaceIsChar = false)
        {
            try
            {

                var dataStream = lorumIpsumDataModel.LoadDataLorumIpsum();
                // convert Data Stream to String
                var dataString = dataService.StreamToString(dataStream);
                //  When blank space considered a character get a total.
                int totalWhiteSpace = WhiteSpaceIsChar ? dataString.Count(Char.IsWhiteSpace) : 0;
                var words = dataService.GetWordDetail(dataString).OrderByDescending(o => o.Length);
                var chars = dataService.GetCharactersFrequency(dataString);

                var result = new LorumIpsumDetailsViewModel
                {
                    TotalWords = words.Sum(a => a.Frequency),
                    Totalcharacters = chars.Sum(s => s.Value) + totalWhiteSpace,
                    FivelargestWords = words.Take(5).Select(w => w.Word).ToList(),
                    FiveSmallestWords = words.TakeLast(5).Select(w => w.Word).ToList(),
                    TenMostFrequentlyWords = words.OrderByDescending(a => a.Frequency).Take(10).Select(w => w.Word).ToList(),
                    Characters = chars.AsQueryable().Select(c => new Entity.Character { Char = c.Key, Frequency = c.Value }).OrderByDescending(o => o.Frequency).ToList()
                };

                return Ok(value: Json(result));
            }
            catch (Exception ex)
            {
                //retorna error 500 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
