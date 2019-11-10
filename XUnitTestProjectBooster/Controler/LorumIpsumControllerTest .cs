using LageHelersonBoosterTest2019.Controllers;
using LageHelersonBoosterTest2019.Model;
using LageHelersonBoosterTest2019.Service;
using LageHelersonBoosterTest2019.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProjectBooster.Controler
{
    public class LorumIpsumControllerTest
    {

        private readonly IDataService dataService;
        private readonly ILorumIpsumDataModel lorumIpsumDataModel;
        private readonly LorumIpsumController lic;       

        public LorumIpsumControllerTest()
        {
            //Dependency Injection
            var services = new ServiceCollection();
            services.AddTransient<IDataService, DataService>();
            // DI - replace data model by fake data model
            services.AddTransient<ILorumIpsumDataModel, FakeLorumIpsumDataModel>();

            var serviceProvider = services.BuildServiceProvider();
            lorumIpsumDataModel = serviceProvider.GetService<ILorumIpsumDataModel>();
            dataService = serviceProvider.GetService<IDataService>();

            lic = new LorumIpsumController(lorumIpsumDataModel, dataService);
        }

        [Fact]
        public void Should_Return_Code200()
        {
            //act
            var okResult = lic.Getdata() as OkObjectResult;
           
            //Assert
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Should_Return_TotalWords_TotalChar_WhiteSpaceIsChar_False()
        {
            //act
            var Result = (lic.Getdata() as OkObjectResult).Value as LorumIpsumDetailsViewModel;

            //assert
            Assert.Equal(22, Result.TotalWords);
            Assert.Equal(99, Result.Totalcharacters);

        }

        [Fact]
        public void Should_Return_TotalWords_TotalChar__WhiteSpaceIsChar_true()
        {
            //act
            var Result = (lic.Getdata(true) as OkObjectResult).Value as LorumIpsumDetailsViewModel;

            //assert
            Assert.Equal(22, Result.TotalWords);
            Assert.Equal(120, Result.Totalcharacters);
        }

        [Fact]
        public void Should_Return_FivelargestWords_and_FiveSmallestWords()
        {
            //act
            var Result = (lic.Getdata(true) as OkObjectResult).Value as LorumIpsumDetailsViewModel;

            //assert
            Assert.Equal(GetFivelargestWords(),  Result.FivelargestWords);
            Assert.Equal(GetFiveSmallestWords(), Result.FiveSmallestWords);
            Assert.Equal(16, Result.Characters.Count);

        }


        #region Private Methods
        private List<String> GetFivelargestWords()
        {
           return new List<String>() { "facilisi", "iaculis", "dolore", "turpis", "tortor" };            
        }

        private List<String> GetFiveSmallestWords()
        {
            return new List<String>() { "odio", "ante", "urna", "sit", "ut" };
        }
        #endregion

    }

}



