using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models;
using DeliveryApp.Domain.Models.CourierAggregate;
using DeliveryApp.Domain.Models.OrderAggregate;

namespace DeliveryApp.Domain.Services
{
    public interface IDispatchService
    {
        Result<Courier, Error> Dispatch(Order order, List<Courier> couriers);
    }
}
