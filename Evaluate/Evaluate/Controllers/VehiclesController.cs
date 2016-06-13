﻿using Repository;
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
        public IEnumerable<Vehicle> Get()
        {
            IEnumerable<Vehicle> vehicles = _inMemoryVehicleRepository.Get();
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            if (allUrlKeyValues != null)
            {
                var dictionary = allUrlKeyValues.ToDictionary((keyItem) => keyItem.Key.ToLower(), (valueItem) => valueItem.Value.ToLower());

                if (dictionary.Keys.Contains("model"))
                {
                    vehicles = vehicles.Where(x => x.Model.ToLower() == dictionary["model"]);
                }
                if (dictionary.Keys.Contains("make"))
                {
                    vehicles = vehicles.Where(x => x.Make.ToLower() == dictionary["make"]);
                }
                if (dictionary.Keys.Contains("year"))
                {
                    vehicles = vehicles.Where(x => x.Year == Convert.ToInt32(dictionary["year"]));
                }
            }
            return vehicles;
        }

        // GET api/vehicles/5
        public Vehicle GetById(int id)
        {
            return _inMemoryVehicleRepository[id];
        }

        // POST api/vehicles
        public void Post([FromBody]Vehicle vehicle)
        {
            _inMemoryVehicleRepository.Add(vehicle);
        }

        // PUT api/vehicles/5
        public void Put([FromBody]Vehicle vehicle)
        {
            _inMemoryVehicleRepository.Add(vehicle);
        }

        // DELETE api/vehicles/5
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
