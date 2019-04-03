using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Clockwork.Web;
using Clockwork.Web.Controllers;

namespace Clockwork.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public async void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = await controller.Index() as ViewResult;

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            var expectedVersion = mvcName.Version.Major + "." + mvcName.Version.Minor;
            var expectedRuntime = isMono ? "Mono" : ".NET";

            // Assert
            Assert.AreEqual(expectedVersion, result.ViewData["Version"]);
            Assert.AreEqual(expectedRuntime, result.ViewData["Runtime"]);
        }
    }
}
