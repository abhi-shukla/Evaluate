using Repository;
using Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Evaluate.Controllers
{
    [EnableCors(origins: "https://estimate-dev.mymitchell.com", headers: "*", methods: "GET, POST, PATCH, PUT, DELETE, OPTIONS")]
    public class VehiclesController : ApiController
    {
        private readonly InMemoryVehicleRepository _inMemoryVehicleRepository;

        public VehiclesController()
        {
            _inMemoryVehicleRepository = InMemoryVehicleRepository.Instance;
        }

        // GET api/vehicles
        [Route("api/vehicles/{make?}/{model?}/{year?}")]
        public IEnumerable<Vehicle> Get(string make = null, string model = null, int year = 0)
        {
            IEnumerable<Vehicle> vehicles = _inMemoryVehicleRepository.Get();
            if (!string.IsNullOrEmpty("model"))
            {
                vehicles = vehicles.Where(x => x.Model.ToLower() == model.ToLower());
            }
            if (!string.IsNullOrEmpty("make"))
            {
                vehicles = vehicles.Where(x => x.Make.ToLower() == make.ToLower());
            }
            if (year > 0)
            {
                vehicles = vehicles.Where(x => x.Year == year);
            }

            return vehicles;
        }

        [Route("api/vehicles")]
        public IEnumerable<Vehicle> Get()
        {
            IEnumerable<Vehicle> vehicles = _inMemoryVehicleRepository.Get();
            return vehicles;
        }

        // GET api/vehicles/5
        [Route("api/vehicles/id")]
        public Vehicle GetById(int id)
        {
            return _inMemoryVehicleRepository[id];
        }

        // POST api/vehicles
        [Route("api/vehicles")]
        public void Post([FromBody]Vehicle vehicle)
        {
            _inMemoryVehicleRepository.Add(vehicle);
        }

        // PUT api/vehicles/5
        [Route("api/vehicles")]
        public void Put([FromBody]Vehicle vehicle)
        {
            _inMemoryVehicleRepository.Add(vehicle);
        }

        // DELETE api/vehicles/5
        [Route("api/vehicles/id")]
        public void Delete(int id)
        {
            _inMemoryVehicleRepository.Remove(id);
        }
    }

    public class QueryFilter
    {
        public string Make;
        public string Model;
        public int Year; 
    }
}
