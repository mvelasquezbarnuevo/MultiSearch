using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MultiSearch.Common.Search;
using MultiSearch.Displays;
using MultiSearch.SearchingCore.Engines;
using SearchFight;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace MultiSearch.UnitTest
{
    [TestClass]
    public class ConsoleHandlerTest
    {
       

        [TestMethod]
        public void GivenOnlyOneSearchCriteriaThenEnginesWontBeLoaded()
        {
            Mock<IEngineLoader> engineLoader = new Mock<IEngineLoader>();
            Mock<IDisplayComparer> displayHandler = new Mock<IDisplayComparer>();
            Mock<ILog> iLog = new Mock<ILog>();

            engineLoader.Setup(l => l.Ready()).Returns(true);
            var sut = new ConsoleHandler(engineLoader.Object, displayHandler.Object, iLog.Object);
            sut.Start(new List<string> { ".net" });

            engineLoader.Verify(e => e.Load(It.IsAny<DirectoryCatalog>()), Times.Never);
        }

       
    }
}
