using System.Xml.Linq;
using WarehouseAPI.DAO;
using WarehouseAPI.Middleware;
using WarehouseAPI.Model;

namespace WarehouseAPI.Services
{
    public interface IWarehouseService
    {
        Task<int> Create(ProductRequest request);
        Task<int> CreateProcedure(ProductRequest request);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseDAO _warehouseDAO;

        public WarehouseService(IWarehouseDAO warehouseDAO)
        {
            _warehouseDAO = warehouseDAO;
        }

        public async Task<int> Create(ProductRequest request)
        {
            bool isRequestBodyValid = await IsValidRequestBody(request);
            if (isRequestBodyValid)
            {
                Order order = await GetOrderForProduct(request);
                if (order != null)
                {
                    await _warehouseDAO.UpdateFullifiledTimeOrder(order);
                    Product product = await _warehouseDAO.GetProductByOrder(order);
                    ProductWarehouse productWarehouse = new(0, request.IdWarehouse, order.IdProduct, order.IdOrder, request.Amount, request.Amount * product.Price, DateTime.Now);
                    return await _warehouseDAO.Create(productWarehouse);
                }
                else
                {
                    throw new NotFoundException("Proper order not found");
                }
            } else
            {
                throw new Exception();
            }
        }

        public async Task<int> CreateProcedure(ProductRequest request)
        {
            return await _warehouseDAO.CreateProcedure(request);
        }

        private async Task<bool> IsValidRequestBody(ProductRequest request)
        {
            bool isProductExists = await _warehouseDAO.CountProductById(request.IdProduct) == 1;
            bool isWarehouseExists = await _warehouseDAO.CountWarehouseById(request.IdWarehouse) == 1;

            if (!isProductExists)
            {
                throw new NotFoundException($"Product with id: {request.IdProduct} not found");
            }

            if (!isWarehouseExists)
            {
                throw new NotFoundException($"Warehouse with id: {request.IdWarehouse} not found");
            }

            return request.Amount > 0;
        }

        private async Task<Order> GetOrderForProduct(ProductRequest request)
        {
            List<Order> orders = await _warehouseDAO.GetOrdersByProductId(request.IdProduct);

            return orders.FirstOrDefault(order => order.Amount == request.Amount && order.CreatedAt < request.CreatedAt && !IsOrderWasRealised(order).ConfigureAwait(false).GetAwaiter().GetResult());
        }

        private async Task<bool> IsOrderWasRealised(Order order)
        {
            return await _warehouseDAO.CountProductWarehouseByOrderId(order.IdOrder) > 0;
        }
    }
}
