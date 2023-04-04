using WarehouseAPI.DAO;
using WarehouseAPI.Model;
using System;

namespace WarehouseAPI.Services
{
    public interface IWarehouseService
    {
        Task<bool> Create(ProductRequest request);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseDAO _warehouseDAO;

        public WarehouseService(IWarehouseDAO warehouseDAO)
        {
            _warehouseDAO = warehouseDAO;
        }

        public async Task<bool> Create(ProductRequest request)
        {
            return await _warehouseDAO.Create(request);
        }
    }
}