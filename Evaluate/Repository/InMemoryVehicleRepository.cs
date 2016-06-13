using System.Collections;
using System.Linq;
using Repository.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Repository
{
    public class InMemoryVehicleRepository : ICollection<Vehicle>
    {
        private static ConcurrentDictionary<int, Vehicle> _vehicles;
        private static InMemoryVehicleRepository _instance;
        private static readonly object Padlock = new object();

        InMemoryVehicleRepository(ConcurrentDictionary<int, Vehicle> vehicle)
        {
            _vehicles = vehicle;
        }

        public static InMemoryVehicleRepository Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ??
                           (_instance = new InMemoryVehicleRepository(new ConcurrentDictionary<int, Vehicle>()));
                }
            }
        }

        public IEnumerable<Vehicle> Get()
        {
            return _vehicles.Values;
        }

        public void Add(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException("vehicle");
            }

            int id;
            if (vehicle.Id <= 0)
            {
                id = GetNewId();
            }
            else
            {
                id = vehicle.Id;
            }
            _vehicles[id] = vehicle.CloneWith(id);
        }

        public Vehicle this[int id]
        {
            get
            {
                Vehicle vehicle;
                if (!_vehicles.TryGetValue(id, out vehicle))
                {
                    throw new ArgumentNullException("id");
                }
                return vehicle;
            }
        }

        public void Clear()
        {
            _vehicles.Clear();
        }

        public bool Contains(Vehicle item)
        {
            return _vehicles.ContainsKey(item.Id);
        }

        public void CopyTo(Vehicle[] array, int arrayIndex)
        {
            _vehicles.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _vehicles.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Vehicle vehicle)
        {
            Vehicle _vehicle;
            return _vehicles.TryRemove(vehicle.Id, out _vehicle);
        }

        public bool Remove(int id)
        {
            Vehicle _vehicle;
            return _vehicles.TryRemove(id, out _vehicle);
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return _vehicles.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetNewId()
        {
            if (_vehicles.Count > 0)
            {
                return _vehicles.Keys.Max<int>() + 1;
            }

            return 1;
        }
    }
}
