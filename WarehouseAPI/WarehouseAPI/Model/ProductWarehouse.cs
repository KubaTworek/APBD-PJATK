namespace WarehouseAPI.Model
{
    public class ProductWarehouse
    {
        public long IdProductWarehouse { get; set; }
        public long IdWarehouse { get; set; }
        public long IdProduct { get; set; }
        public long IdOrder { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductWarehouse(long idProductWarehouse, long idWarehouse, long idProduct, long idOrder, int amount, double price, DateTime createdAt)
        {
            IdProductWarehouse = idProductWarehouse;
            IdWarehouse = idWarehouse;
            IdProduct = idProduct;
            IdOrder = idOrder;
            Amount = amount;
            Price = price;
            CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"ProductWarehouse(IdProductWarehouse={IdProductWarehouse}, IdWarehouse={IdWarehouse}, IdProduct={IdProduct}, IdOrder={IdOrder}, Amount={Amount}, Price={Price}, CreatedAt={CreatedAt})";
        }
    }
}