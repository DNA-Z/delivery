using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Location Location { get;}
        public int Volume { get;}
        public OrderStatus? Status { get;}
        public Guid CourierId { get; }

        private Order() { }

        private Order(Guid orderId, Location location, int volume)
        {
            CourierId = orderId;
            Location = location;
            Volume = volume;
        }

        public static Result<Order> Create (Guid orderId, Location location, int volume)
        {
            if () return ResultWithError.Fail("some error info");
        }

        public UnitResult<Error>
    }
}
