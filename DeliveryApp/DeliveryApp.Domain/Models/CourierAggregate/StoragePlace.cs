using CSharpFunctionalExtensions;

namespace DeliveryApp.Domain.Models.CourierAggregate
{
    public class StoragePlace : Entity<Guid>
    {
        public Guid Id { get; private set; }
        public StoragePlaceName Name { get; private set; }
        public int TotalVolume { get; private set; }
        public Guid? OrderId { get; private set; }

        private StoragePlace() { }

        private StoragePlace(StoragePlaceName name, int volume)
        {
            Name = name;
            TotalVolume = volume;
        }

        public static Result<StoragePlace, Error> Create(StoragePlaceName name, int volume)
        {
            if (name is null) return Errors.NameCannotBeNull(name);
            if (volume < 0) return Errors.VolumeCannotBeLessThanZero(volume);
            if (volume == 0) return Errors.VolumeCannotBeEqualToZero(volume);
            if (volume > 10) return Errors.VolumeCannotBeMoreThanTen(volume);

            return new StoragePlace(StoragePlaceName.Bag, volume);
        }

        public Result<bool, Error> CanStore(int volume)
        {
            if (volume > TotalVolume)
            {
                return false;
            }

            return true;
        }

        public Result<object, Error> Store(Guid orderId, int volume)
        {
            if (OrderId is not null && CanStore(volume).Value is false)
            {
                return Errors.OrderCannotBeAdded(orderId);
            }

            OrderId = orderId;

            return new object();
        }

        public void Clear(Guid orderId)
        {
            if (orderId == OrderId)
            {
                Name = StoragePlaceName.Bag;
                TotalVolume = 10;
                OrderId = null;
            }
        }

        public static class Errors
        {
            public static Error NameCannotBeNull(StoragePlaceName? name)
            {
                return new Error($"{nameof(name)} can't be null");
            }

            public static Error VolumeCannotBeLessThanZero(int volume)
            {
                return new Error($"{nameof(volume)} can't be less than 0");
            }

            public static Error VolumeCannotBeEqualToZero(int volume)
            {
                return new Error($"{nameof(volume)} can't be equal to 0");
            }

            public static Error VolumeCannotBeMoreThanTen(int volume)
            {
                return new Error($"{nameof(volume)} can't be more than 10");
            }

            public static Error OrderCannotBeAdded(Guid orderId)
            {
                return new Error($"Order {orderId} can't be added");
            }
        }
    }
}
