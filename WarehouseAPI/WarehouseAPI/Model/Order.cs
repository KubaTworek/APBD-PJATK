namespace WarehouseAPI.Model
{
    public class Order
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FulfilledAt { get; set; }

        public Order(int idOrder, int idProduct, int amount, DateTime createdAt)
        {
            IdOrder = idOrder;
            IdProduct = idProduct;
            Amount = amount;
            CreatedAt = createdAt;
        }

        public Order(int idOrder, int idProduct, int amount, DateTime createdAt, DateTime fulfilledAt) : this(idOrder, idProduct, amount, createdAt)
        {
            FulfilledAt = fulfilledAt;
        }
    }
}
