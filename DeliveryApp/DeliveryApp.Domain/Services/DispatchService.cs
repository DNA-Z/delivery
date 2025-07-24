using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models;
using DeliveryApp.Domain.Models.CourierAggregate;
using DeliveryApp.Domain.Models.OrderAggregate;

namespace DeliveryApp.Domain.Services
{
    public class DispatchService : IDispatchService
    {
        public Result<Courier, Error> Dispatch(Order order, List<Courier> couriers)
        {
            if (order.Status != OrderStatus.Created) 
                return new Error("Order status is not \"сreated\"");

            var readyCouriers = couriers
                .Where(x => x.CanTakeOrder(order).Value).ToList();

            var courier = readyCouriers
                .MinBy(x => x.Location.DistanceTo(order.Location).Value);

            if (courier is null)
                return new Error("All couriers are busy. Please wait.");

            courier.TakeOrder(order);

            return courier;
        }
    }
}