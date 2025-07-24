
namespace DeliveryApp.Domain.Models
{
    public readonly struct Error
    {
        public string Message { get; }
        public Error(string message) => Message = message;
        public override string ToString() => Message;
    }
}
