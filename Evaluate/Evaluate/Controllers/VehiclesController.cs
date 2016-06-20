using Repository;
using Repository.Models;
using System.Collections.Generic;
using System.Linq;
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

        [Route("api/vehicles/{make?}/{model?}/{year?}")]
        public IEnumerable<Vehicle> Get(string make = null, string model = null, int year = 0)
        {
            IEnumerable<Vehicle> vehicles = _inMemoryVehicleRepository.Get();
            if (!string.IsNullOrEmpty(model))
            {
                vehicles = vehicles.Where(x => x.Model.ToLower() == model.ToLower());
            }
            if (!string.IsNullOrEmpty(make))
            {
                vehicles = vehicles.Where(x => x.Make.ToLower() == make.ToLower());
            }
            if (year > 0)
            {
                vehicles = vehicles.Where(x => x.Year == year);
            }

            return vehicles;
        }

        [Route("api/vehicles/id")]
        public Vehicle Get(int id)
        {
            return _inMemoryVehicleRepository.Get().FirstOrDefault(x => x.Id == id);
        }

        [Route("api/vehicles/id")]
        public Vehicle GetById(int id)
        {
            return _inMemoryVehicleRepository[id];
        }

        [Route("api/vehicles")]
        public void Post([FromBody]Vehicle vehicle)
        {
            _inMemoryVehicleRepository.Add(vehicle);
        }

        [Route("api/vehicles")]
        public void Put([FromBody]Vehicle vehicle)
        {
            _inMemoryVehicleRepository.Add(vehicle);
        }

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
