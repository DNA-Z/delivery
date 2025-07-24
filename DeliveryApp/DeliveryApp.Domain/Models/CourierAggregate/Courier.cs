using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models.OrderAggregate;
using DeliveryApp.Domain.Models.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeliveryApp.Domain.Models.CourierAggregate
{
    public class Courier
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Speed { get; private set; }
        public Location Location { get; private set; }
        public List<StoragePlace> StoragePlaces { get; private set; }

        private Courier() { }

        private Courier(string name, int speed, Location location, StoragePlace storagePlace)
        {
            Id = new Guid();
            Name = name;
            Speed = speed;
            Location = location;
            StoragePlaces = new List<StoragePlace>();
        }

        public static Result<Courier> Create(string name, int speed, Location location)
        {
            if (name == null) throw new ArgumentNullException($"{nameof(name)} can't be null");
            if (speed < 0) throw new ArgumentOutOfRangeException($"{nameof(speed)} can't be less than 0");
            if (speed == 0) throw new ArgumentOutOfRangeException($"{nameof(speed)} can't be equal to 0");
            if (location == null) throw new ArgumentNullException($"{nameof(location)} can't be null");

            var storagePlace = new StoragePlace(name, speed, location, new StoragePlace());

            return new Courier(name, speed, location, storagePlace);
        }

        public UnitResult<Error> AddStoragePlace(string name, int volume)
        {

        }

        public Result<Error> CanTakeOrder(Order order)
        {

        }

        public UnitResult<Error> TakeOrder(Order order)
        {

        }

        public UnitResult<Error> AddStoragePlace(string name, int volume)
        {

        }

        public UnitResult<Error> AddStoragePlace(string name, int volume)
        {

        }

        public UnitResult<Error> AddStoragePlace(string name, int volume)
        {

        }


        public static class Errors
        {
            public static Error CourierError()
            {
                return new Error($"{nameof(Courier).ToLowerInvariant()}", "Discription error");
            }
        }
    }
}