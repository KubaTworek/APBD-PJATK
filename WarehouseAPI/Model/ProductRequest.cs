using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Model
{
    public class ProductRequest
    {
        public long IdProduct { get; set; }
        public long IdWarehouse { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt{ get; set; }

        public ProductRequest(long idProduct, long idWarehouse, int amount, DateTime createdAt)
        {
            IdProduct = idProduct;
            IdWarehouse = idWarehouse;
            Amount = amount;
            CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"Product Request: IdProduct={IdProduct}, IdWarehouse={IdWarehouse}, Amount={Amount}, CreatedAt={CreatedAt}";
        }
    }
}
