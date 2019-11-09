using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LageHelersonBoosterTest2019.Model;
using LageHelersonBoosterTest2019.Service;
using LageHelersonBoosterTest2019.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LageHelersonBoosterTest2019.Controllers
{
    //[Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly IDataService dataService;
        private readonly ILorumIpsumDataModel lorumIpsumDataModel;

        public ValuesController(ILorumIpsumDataModel lorumIpsumDataModel, IDataService dataService)
        {
            this.dataService = dataService;
            this.lorumIpsumDataModel = lorumIpsumDataModel;
        }

        [HttpGet]
        [Route("api/GetlorumIpsumDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Getdata()
        {
            try
            {
                var dataStream = lorumIpsumDataModel.LoadDataLorumIpsum();
                var dataString = dataService.StreamToString(dataStream);
                var words = dataService.GetWordDetail(dataString);

                var result = new LorumIpsumDetailsViewModel
                {
                    TotalWords = words.Sum(a => a.Frequency),
                    Totalcharacters = words.Sum(a => a.Length * a.Frequency),
                    FivelargestWord = words.OrderByDescending(a => a.Length).Take(5).Select(w => w.Word).ToList(),
                    FiveSmallestWord = words.OrderBy(a => a.Length).Take(5).Select(w => w.Word).ToList(),
                    TenMostFrequentlyWord = words.OrderByDescending(a => a.Frequency).Take(10).Select(w => w.Word).ToList(),

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
