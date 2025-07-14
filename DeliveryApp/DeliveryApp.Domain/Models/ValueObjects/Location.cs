using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Models.ValueObjects
{
    public class Location : ValueObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private Location() { }

        public Location(int x, int y)
        {
            SetX(x);
            SetY(y);
        }

        public static Result<Location> Create(int x, int y)
        {
            var result = new Location(x, y);
            return result;
        }

        public static Result<Location> CreateRandom()
        {
            var random = new Random();
            return new Location(random.Next(1, 100), random.Next(1, 100));
        }

        private void SetX(int x) 
        {
            if (x <= 0)
                throw new ArgumentOutOfRangeException(nameof(x));

            X = x;
        }

        private void SetY(int y)
        {
            if (y <= 0)
                throw new ArgumentOutOfRangeException(nameof(y));

            Y = y;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }
    }
}
