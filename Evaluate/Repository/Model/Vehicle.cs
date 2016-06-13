namespace Repository.Models
{
    public class Vehicle
    {
        public int Id { get; private set; }

        public string Model { get; private set; }

        public string Make { get; private set; }

        public int Year { get; private set; }

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