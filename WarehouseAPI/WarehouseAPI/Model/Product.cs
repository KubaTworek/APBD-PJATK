namespace WarehouseAPI.Model
{
    public class Product
    {
        public long IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Product(long idProduct, string name, string description, double price)
        {
            IdProduct = idProduct;
            Name = name;
            Description = description;
            Price = price;
        }

        public override string ToString()
        {
            return $"[Id: {IdProduct}, Name: {Name}, Description: {Description}, Price: {Price}]";
        }
    }
}