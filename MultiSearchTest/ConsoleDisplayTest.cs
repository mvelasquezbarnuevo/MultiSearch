using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MultiSearch.Common.Formatters;
using MultiSearch.Displays;
using MultiSearch.SearchingCore.Engines;
using System.Collections.Generic;

namespace MultiSearchTest
{
    [TestClass]
    public class ConsoleDisplayTest
    {
        [TestMethod]
        public void GivenAtLeastOneEngineItWillBeShownOnScreen()
        {
            Mock<IEngineLoader> engineLoader = new Mock<IEngineLoader>();
            Mock<IFlavor> screenFlavor = new Mock<IFlavor>();
            Mock<ILog> iLog = new Mock<ILog>();

            var engine = new PluginDescriptor();
            engine.Name = "Google";
            engineLoader.Setup(l => l.GetAvailablePlugins()).Returns(new List<PluginDescriptor> { engine });

            var sut = new ConsoleDisplay(screenFlavor.Object,iLog.Object);
            sut.ShowAvailableEngines(engineLoader.Object);
            screenFlavor.Verify(p => p.FormatAndPrintDescriptor(It.IsAny<PluginDescriptor>()), Times.AtLeastOnce);
        }
    }
}
