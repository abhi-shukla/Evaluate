using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evaluation;
using Repository.Models;
using Evaluate.Controllers;

namespace Evaluation.Tests.Controllers
{
    [TestClass]
    public class VehiclesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var controller = new VehiclesController();

            // Act
            IEnumerable<Vehicle> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();

            // Act
            var result = controller.Get(5);

            // Assert
            //Assert.AreEqual(new Vehicle(), result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();

            // Act
            //controller.Post(new Vehicle());

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();

            // Act
            //controller.Put(5, new Vehicle());

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
