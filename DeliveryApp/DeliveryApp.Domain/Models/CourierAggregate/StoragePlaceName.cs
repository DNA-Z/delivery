using CSharpFunctionalExtensions;
using DeliveryApp.Domain.Models.OrderAggregate;

namespace DeliveryApp.Domain.Models.CourierAggregate
{
    public class StoragePlaceName : ValueObject
    {
        public static StoragePlaceName Bag => new(nameof(Bag).ToLowerInvariant());
        public static StoragePlaceName Trunk => new(nameof(Trunk).ToLowerInvariant());
        public static StoragePlaceName Backpack => new(nameof(Backpack).ToLowerInvariant());

        public string Name { get; private set; } = string.Empty;

        private StoragePlaceName() { }

        private StoragePlaceName(string name) : this()
        {
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
