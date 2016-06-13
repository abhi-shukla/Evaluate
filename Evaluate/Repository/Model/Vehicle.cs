using System;

namespace Repository.Models
{
    public class Vehicle
    {
        int id;
        string model;
        string make;
        int year;


        public int Id {
            get { return id; }

            private set
            {
                if (id < 0)
                    throw new ArgumentNullException("Id");
                else
                    id = value;
            }
        }

        public string Model {
            get { return model; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Model");
                else
                    model = value;
            }
        }

        public string Make
        {
            get { return make; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Make");
                else
                    make = value;
            }
        }

        public int Year {
            get { return year; }

            private set
            {
                if (value < 1950 || value > 2050)
                    throw new ArgumentNullException("Year");
                else
                    year = value;
            }
        }

        public Vehicle(int id, string model, string make, int year)
        {
            Id = id;
            Model = model;
            Make = make;
            Year = year;
        }

        public Vehicle CloneWith(int id, string model = null, string make = null, int year = 0)
        {
            return new Vehicle
            (
                id,
                model ?? Model,
                make ?? Make,
                year == 0 ? Year : year
            );
        }
    }
}