using CSharpFunctionalExtensions;

namespace DeliveryApp.Domain.Models.SharedKernel
{
    public class Location : ValueObject
    {
        public int X { get; private set; } = 1;
        public int Y { get; private set; } = 1;

        private Location() { }

        public Location(int x, int y)
        {
            SetX(x);
            SetY(y);
        }

        public static Result<Location, Error> Create(int x, int y)
        {
            var result = new Location(x, y);
            return result;
        }

        public Result<int, Error> DistanceTo(Location target)
        {
            var distance = Math.Abs(target.X - X);
            distance += Math.Abs(target.Y - Y);

            return distance;
        }

        public Result<object, Error> ChangeOnOneStep(Location target)
        {
            if (target.X > X) X++;
            if (target.Y > Y) Y++;
            if (target.X < X) X--;
            if (target.Y < Y) Y--;

            return new object();
        }

        public static Result<Location> CreateRandom()
        {
            var random = new Random();
            return new Location(random.Next(1, 11), random.Next(1, 11));
        }

        private Result<object, Error> SetX(int x) 
        {
            if (x == 0) return Errors.LocationCannotBeEqualToZero(x);
            if (x < 0) return Errors.LocationCannotBeLessThanZero(x);
            if (x > 10) return Errors.LocationCannotBeMoreThanTen(x);

            X = x;

            return new object();
        }

        private Result<object, Error> SetY(int y)
        {
            if (y == 0) return Errors.LocationCannotBeEqualToZero(y);
            if (y < 0) return Errors.LocationCannotBeLessThanZero(y);
            if (y > 10) return Errors.LocationCannotBeMoreThanTen(y);

            Y = y;

            return new object();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }

        public static class Errors
        {
            public static Error LocationCannotBeEqualToZero(int position)
            {
                return new Error($"{nameof(position)} can't be equal to 0");
            }

            public static Error LocationCannotBeLessThanZero(int position)
            {
                return new Error($"{nameof(position)} can't be less than 0");
            }

            public static Error LocationCannotBeMoreThanTen(int position)
            {
                return new Error($"{nameof(position)} can't be more than 10");
            }
        }
    }
}
