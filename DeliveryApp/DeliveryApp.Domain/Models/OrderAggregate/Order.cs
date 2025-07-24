using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models.CourierAggregate;
using DeliveryApp.Domain.Models.SharedKernel;

namespace DeliveryApp.Domain.Models.OrderAggregate
{
    public class Order : Entity<Guid>
    {
        public Guid Id { get; set; }
        public Location Location { get; }
        public int Volume { get; }
        public OrderStatus Status { get; private set; }
        public Guid? CourierId { get; private set; }

        private Order() { }

        private Order(Guid orderId, Location location, int volume)
        {
            Id = orderId;
            Location = location;
            Volume = volume;
            Status = OrderStatus.Created;
        }

        public static Result<Order, Error> Create(Guid? orderId, Location? location, int volume)
        {
            if (orderId is null) return Errors.OrderIdCannotBeNull();
            if (location is null) return Errors.LocationCannotBeNull();
            if (volume == 0) return Errors.VolumeCannotBeEqualToZero();
            if (volume < 0) return Errors.VolumeCannotBeEqualToZero();

            return new Order(orderId.Value, location, volume);
        }

        public Result<object, Error> Assign(Courier? courier)
        {
            if (courier is null) return Errors.CourierIdCannotBeNull();

            CourierId = courier.Id;
            Status = OrderStatus.Assigned;

            return new object();
        }

        public Result<object, Error> Complete()
        {
            if (Status != OrderStatus.Assigned) return new Error("Unassigned order cannot be completed");
            Status = OrderStatus.Completed;
            return new object();
        }

        public static class Errors
        {
            public static Error OrderIdCannotBeNull()
                => new ($"In {nameof(Order).ToLowerInvariant()} order id can't be null");

            public static Error LocationCannotBeNull()
                => new ($"In {nameof(Order).ToLowerInvariant()} location can't be null");

            public static Error VolumeCannotBeEqualToZero()
                => new ($"In {nameof(Order).ToLowerInvariant()} volume can't be equal to 0");

            public static Error VolumeCannotBeLessThanZero()
                => new ($"In {nameof(Order).ToLowerInvariant()} volume can't be less than 0");

            public static Error CourierIdCannotBeNull()
                => new($"In {nameof(Order).ToLowerInvariant()} courier id can't be null");
        }
    }
}
