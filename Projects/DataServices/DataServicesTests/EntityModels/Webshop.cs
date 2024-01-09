namespace SQLiteDemoTests.EntityModels
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
    }

    public class Customer : Entity
    {
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Address Address { get; set; }
    }

    public class Order : Entity
    {
        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }
    }

    public class Partner : Entity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class Product : Entity
    {
        public ProductClass ProductClass { get; set; }
        public ICollection<Partner> Partners { get; set; }
        public int Price { get; set; }
    }

    public enum ProductClass
    {
        Mobile, Tv, SportGear, Home, Garder
    }

    public class Address
    {
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
