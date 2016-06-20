using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Models;
using Evaluate.Controllers;
using Repository;

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
            var vehicle1 = new Vehicle(1, "bats", "batmobile", 2005);
            var vehicle2 = new Vehicle(2, "Chrysler", "The Green Hornet", 1966);
            InMemoryVehicleRepository.Instance.Add(vehicle1);
            InMemoryVehicleRepository.Instance.Add(vehicle2);

            // Act
            IEnumerable<Vehicle> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(vehicle1.Id, result.ElementAt(0).Id);
            Assert.AreEqual(vehicle2.Id, result.ElementAt(1).Id);
        }

        [TestMethod]
        public void GetByModel()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();
            var vehicle = new Vehicle(1, "bats", "batmobile", 2016);
            InMemoryVehicleRepository.Instance.Add(vehicle);

            // Act
            var result = controller.Get(model: vehicle.Model);

            // Assert
            Assert.AreEqual(vehicle.Id, result.ToList()[0].Id);
        }

        [TestMethod]
        public void GetByMake()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();
            var vehicle = new Vehicle(1, "bats", "batmobile", 2016);
            InMemoryVehicleRepository.Instance.Add(vehicle);

            // Act
            var result = controller.Get(make: vehicle.Make);

            // Assert
            Assert.AreEqual(vehicle.Id, result.ToList()[0].Id);
        }

        [TestMethod]
        public void GetByYear()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();
            var vehicle = new Vehicle(1, "bats", "batmobile", 2016);
            InMemoryVehicleRepository.Instance.Add(vehicle);

            // Act
            var result = controller.Get(year: vehicle.Year);

            // Assert
            Assert.AreEqual(vehicle.Id, result.ToList()[0].Id);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();
            var vehicle = new Vehicle(1, "bats", "batmobile", 2016);

            // Act
            controller.Post(vehicle);
            var result = controller.Get(vehicle.Id);

            // Assert
            Assert.AreEqual(vehicle.Year, result.Year);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();
            var vehicle = new Vehicle(1, "bats", "batmobile", 2016);
            InMemoryVehicleRepository.Instance.Add(vehicle);

            // Act
            var updatedVehicle = vehicle.CloneWith(vehicle.Id, year: 2010);
            controller.Put(updatedVehicle);
            var result = controller.Get(vehicle.Id);

            // Assert
            Assert.AreEqual(updatedVehicle.Year, result.Year);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();
            var vehicle = new Vehicle(1, "bats", "batmobile", 2016);
            InMemoryVehicleRepository.Instance.Add(vehicle);

            // Act
            controller.Delete(vehicle.Id);
            var result = controller.Get(vehicle.Id);

            //Assert
            Assert.IsNull(result);
        }
    }
}
