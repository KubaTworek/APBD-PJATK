using AnimalAPI.DAO;
using AnimalAPI.Model;
using System;

namespace AnimalAPI.Services
{
    public interface IAnimalService
    {
        Task<IList<Animal>> GetAll(string orderBy);
        Task<bool> Create(Animal animal);
        Task<bool> Update(Animal animal, long id);
        Task<bool> Delete(long id);
    }

    public class AnimalService : IAnimalService
    {
        private readonly IAnimalDAO _animalDAO;

        public AnimalService(IAnimalDAO animalDAO)
        {
            _animalDAO = animalDAO;
        }

        public async Task<IList<Animal>> GetAll(string orderBy)
        {
            return await _animalDAO.GetAll(orderBy);
        }

        public async Task<bool> Create(Animal animal)
        {
            return await _animalDAO.Create(animal);
        }

        public async Task<bool> Update(Animal animal, long id)
        {
            return await _animalDAO.Update(animal, id);
        }

        public async Task<bool> Delete(long id)
        {
            return await _animalDAO.Delete(id);
        }
    }
}