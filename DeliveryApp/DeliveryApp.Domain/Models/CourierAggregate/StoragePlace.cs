using CSharpFunctionalExtensions;

namespace DeliveryApp.Domain.Models.CourierAggregate
{
    public class StoragePlace : Entity<Guid>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int TotalVolume { get; private set; }
        public Guid? OrderId { get; private set; }

        private StoragePlace() { }

        private StoragePlace(string name, int volume)
        {
            Name = name;
            TotalVolume = volume;
        }

        public static Result<StoragePlace, Error> Create(string name, int volume)
        {
            if (string.IsNullOrWhiteSpace(name)) return Errors.NameCannotBeNull(name);
            if (volume < 0) return Errors.VolumeCannotBeLessThanZero(volume);
            if (volume == 0) return Errors.VolumeCannotBeEqualToZero(volume);

            return new StoragePlace(name, volume);
        }

        public Result<bool, Error> CanStore(int volume)
        {
            if (OrderId == null || volume > TotalVolume)
            {
                return false;
            }

            var r = new Result();
            var e = r.Error;

            return true;
        }

        public UnitResult<Error> Store(Guid orderId, int volume)
        {
            if (orderId == OrderId && CanStore(volume).IsFailure)
            {
                return Errors.OrderCannotBeAdded(orderId);
            }

            return new Error("");
        }

        public void Clear(Guid orderId)
        {
            if (orderId == OrderId)
            {
                Name = string.Empty;
                TotalVolume = 0;
                OrderId = null;
            }
        }

        public static class Errors
        {
            public static Error NameCannotBeNull(string? name)
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

            public static Error OrderCannotBeAdded(Guid orderId)
            {
                return new Error($"Order {orderId} can't be added");
            }
        }
    }
}
