using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.Domain.Models.OrderAggregate
{
    public class OrderStatus : ValueObject
    {
        public static OrderStatus Created => new(nameof(Created).ToLowerInvariant());
        public static OrderStatus Assigned => new(nameof(Assigned).ToLowerInvariant());
        public static OrderStatus Completed => new(nameof(Completed).ToLowerInvariant());

        public string Name { get; private set; } = string.Empty;

        [ExcludeFromCodeCoverage]
        private OrderStatus() { }

        private OrderStatus(string name) : this()
        {
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
