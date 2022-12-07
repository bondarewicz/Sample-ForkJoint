namespace ForkJoint.Contracts
{
    using System;


    public record Burger
    {
        public Guid BurgerId { get; init; }
        public decimal Weight { get; init; }
        public bool Lettuce { get; init; }
        public bool Cheese { get; init; }
        public bool Pickle { get; init; }
        public bool Onion { get; init; }
        public bool Ketchup { get; init; }
        public bool Mustard { get; init; }
    }
}