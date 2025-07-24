
namespace DeliveryApp.Domain.Models
{
    public readonly struct Error
    {
        public string Message { get; }
        public string? Description { get; }
        public Error(string message) => Message = message;
        public Error(string message, string description)
        {
            Message = message;
            Description = description;
        }
        public override string ToString() => $"{Message}: {Description ?? string.Empty}";
    }
}