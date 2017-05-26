


using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MultiSearch.Common.Formatters;
using MultiSearch.Common.Search;
using MultiSearch.SearchingCore.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearchTest
{
    [TestClass]
    public class EngineLoaderTest
    {

        [TestMethod]
        public void GivenAValidDirectoryPluginsLoadShouldShowThemAsAvailables()
        {
            Mock<ILog> iLog = new Mock<ILog>();
            var sut = new EngineLoader(iLog.Object);
            sut.Load(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            Assert.AreEqual(sut.Ready(), true);
        }


        [TestMethod]
        public void GivenAnInvalidDirectoryPluginsLoadShouldShowThemAsNotAvailables()
        {
            Mock<ILog> iLog = new Mock<ILog>();
            var sut = new EngineLoader(iLog.Object);
            sut.Load(new DirectoryCatalog(Path.GetDirectoryName("c:\\temp")));

            Assert.AreEqual(sut.Ready(), false);
        }

        [TestMethod]
        public void GivenNullParametersShouldReturnEmptyResponse()
        {
            Mock<ILog> iLog = new Mock<ILog>();
            var sut = new EngineLoader(iLog.Object);

            var returnedValue = sut.FormatRespose(null, null);

            var expected = new ContestResponse
            {
                EngineResponses = null,
            };

            Assert.AreEqual(returnedValue.EngineResponses, expected.EngineResponses);

        }

        [TestMethod]
        public void GivenNullParametersShouldReturnValidResponse()
        {
            Mock<ILog> iLog = new Mock<ILog>();
            var sut = new EngineLoader(iLog.Object);

            var request = new SearchRequest
            {
                SearchBy = ".net"
            };
            var response = new SearchResponse
            {
                Word = ".net",
                EngineName = "Google",
                RecordsCount = 10000
            };

            var returnedValue = sut.FormatRespose(request, new SearchResponse[] { response });

            var expected = new ContestResponse
            {
                EngineResponses = null,
            };

            Assert.AreNotEqual(returnedValue.EngineResponses, expected.EngineResponses);

        }
    }
}
