using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models.OrderAggregate;
using DeliveryApp.Domain.Models.SharedKernel;
using System.Xml.Linq;

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
            StoragePlaces = new() { storagePlace };
        }

        public static Result<Courier, Error> Create(string name, int speed, Location location)
        {
            if (name is null) return Errors.CannotBeNull(nameof(name));
            if (speed < 0) return Errors.IntCannotBeLessThanZero(nameof(speed));
            if (speed == 0) Errors.IntCannotBeLessThanZero(nameof(speed));
            if (location is null) return Errors.CannotBeNull(nameof(location));

            var storagePlace = StoragePlace.Create(StoragePlaceName.Bag, 10).Value;

            return new Courier(name, speed, location, storagePlace);
        }

        public Result<object, Error> AddStoragePlace(StoragePlaceName name, int volume)
        {
            if (name is null) return Errors.CannotBeNull(nameof(name));
            if (volume < 0) return Errors.IntCannotBeLessThanZero(nameof(volume));
            if (volume == 0) Errors.IntCannotBeLessThanZero(nameof(volume));

            var newStoragePlace = StoragePlace.Create(name, volume).Value;
            StoragePlaces.Add(newStoragePlace);
            return new object();
        }

        public Result<Guid?, Error> CanTakeOrder(Order order)
        {
            if (order is null) return Errors.CannotBeNull(nameof(order));

            var freeSpace = StoragePlaces.FirstOrDefault(x => x.CanStore(order.Volume).Value);

            return freeSpace?.Id;
        }

        public Result<object, Error> TakeOrder(Order order)
        {
            if (order is null) return Errors.CannotBeNull(nameof(order));

            var canFreeSpace = CanTakeOrder(order).Value;

            if (canFreeSpace is not null)
            {
                StoragePlaces.ForEach(x => x.Store(order.Id, order.Volume));
            }

            return new object();
        }

        public Result<object, Error> CompleteOrder(Order order)
        {
            if (order is null) return Errors.CannotBeNull(nameof(order));
            StoragePlaces.ForEach(x => x.Clear(order.Id));
            return new object();
        }

        public Result<double, Error> CalculateTimeToLocation(Location targetLocation)
        {
            if (targetLocation is null) return Errors.CannotBeNull(nameof(targetLocation));
            var steps = Location.DistanceTo(targetLocation).Value;
            double time = steps / Speed;

            return time;
        }

        public Result<object, Error> Move(Location target)
        {
            if (target is null) return Errors.CannotBeNull(nameof(target));
            Location.ChangeOnOneStep(target);

            return new object();
        }

        private static class Errors
        {
            public static Error CourierError()
                => new ($"{nameof(Courier).ToLowerInvariant()}", "Discription error");

            public static Error CannotBeNull(string field)
                => new ($"{field} can't be null");

            public static Error IntCannotBeLessThanZero(string field)
                => new ($"{field} can't be less than 0");

            public static Error IntCannotBeEqualToZero(string field)
                => new ($"{field} can't be equal to 0");
        }
    }
}