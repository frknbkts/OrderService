namespace OrderService.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        private Address() { }

        public static Address Create(
            string street,
            string city,
            string state,
            string country,
            string zipCode)
        {
            return new Address
            {
                Street = street,
                City = city,
                State = state,
                Country = country,
                ZipCode = zipCode
            };
        }
    }
} 