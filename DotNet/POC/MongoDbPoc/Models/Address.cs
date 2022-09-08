namespace MongoDbPoc.Models
{
    internal class Address
    {
        public string RecId { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public override string ToString()
        {
            return $"{ZipCode} {Country} {City} {AddressLine1} {AddressLine2}";
        }
    }
}
