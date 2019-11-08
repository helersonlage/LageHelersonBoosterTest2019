using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LageHelersonBoosterTest2019.Model;
using LageHelersonBoosterTest2019.Service;
using LageHelersonBoosterTest2019.ViewModel;
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
            this.dataService =  dataService;
            this.lorumIpsumDataModel = lorumIpsumDataModel;
        }

        [HttpGet]
        [Route("api/GetlorumIpsumDetails")]
        public ActionResult Getdata()
        {

            var dataStream = lorumIpsumDataModel.LoadDataLorumIpsum();
            var dataString = dataService.StreamToString(dataStream);
            var words = dataService.GetWordDetail(dataString);

            var result = new LorumIpsumDetailsViewModel
            {
                TotalWords = words.Count(),
                Totalcharacters = words.Sum(a => a.Length),
                Top5largestWord = words.OrderByDescending(a => a.Length).Take(5).Select(w => w.Word).ToList(),
                Top5SmallestWord = words.OrderBy(a => a.Length).Take(5).Select(w => w.Word).ToList(),

            };
            return Ok(Json(result));


        }

    }
}
