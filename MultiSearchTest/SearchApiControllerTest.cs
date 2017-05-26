using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MultiSearch.Api.Controllers;
using MultiSearch.Api.Models;
using MultiSearch.Common.Contracts;
using MultiSearch.Common.Formatters;
using MultiSearch.Common.Search;
using MultiSearch.SearchingCore.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace MultiSearchTest
{

    [TestClass]
    public class SearchApiControllerTest
    {

        [TestMethod]
        public async Task GivenaValidRequestShouldResponseWithOk()
        {
            Mock<IEngineLoader> loader = new Mock<IEngineLoader>();
            var sut = new SearchController(loader.Object);
            sut.Configuration = new System.Web.Http.HttpConfiguration();

            var input = new InputParams
            {
                Words = new string[] { ".net" }
            };

            var response = new ContestResponse
            {
                EngineResponses = new List<ISearchResponse>() { new SearchResponse {
                    EngineName = "Google", RecordsCount = 100,
                    Word = ".net" } },
                Engines = new List<string>() { "Google" },
                Words = new List<string>() { ".net" },
            };


            loader.Setup(m => m.SendRequest(It.IsAny<ISearchRequest>(), It.IsAny<List<IPluginComponent>>()))
                .Returns(Task.FromResult(response));

            var actionresult = await sut.GetList(input);

            Assert.AreEqual(actionresult.GetType(), typeof(OkNegotiatedContentResult<ApiResult>));

        }


        [TestMethod]
        public async Task GivenaValidRequestShouldResponsWinner()
        {
            Mock<IEngineLoader> loader = new Mock<IEngineLoader>();
            var sut = new SearchController(loader.Object);
            sut.Configuration = new System.Web.Http.HttpConfiguration();

            var input = new InputParams
            {
                Words = new string[] { ".net" }
            };

            var response = new ContestResponse
            {
                EngineResponses = new List<ISearchResponse>() { new SearchResponse { EngineName = "Google", RecordsCount = 100, Word = ".net" } },
                Engines = new List<string>() { "Google" },
                Words = new List<string>() { ".net" },
            };


            loader.Setup(m => m.SendRequest(It.IsAny<ISearchRequest>(), It.IsAny<List<IPluginComponent>>()))
                .Returns(Task.FromResult(response));

            var actionresult = await sut.GetList(input);

            var responseContent = actionresult as OkNegotiatedContentResult<ApiResult>;
            Assert.AreEqual(responseContent.Content.WinnerWord, ".net");

        }
    }
}
