using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CareTrackerV1;
using CareTrackerV1.Controllers;

namespace CareTrackerV1UnitTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        [Timeout(2000)]
        public void CareGiverController()
        {
            //Arrange
            CareGiverController controller = new CareGiverController();

            //Act
            ViewResult result = controller.Index(1,1) as ViewResult;    //Index values must match table data

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NextOfKin()
        {
            //Arrange
            NextOfKinController controller = new NextOfKinController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Chart1()
        {
            //Arrange
            CareGiverController controller = new CareGiverController();

            //Act
            ViewResult result = controller.Chart1() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
