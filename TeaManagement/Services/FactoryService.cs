using System;
using System.Threading.Tasks;
using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;


namespace TeaManagement.Services
{
    public class FactoryService : IFactoryService
    {
        private readonly AppDbContext _context;

        public FactoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NewFactory> GetFactoryByIdAsync(int id)
        {
            var factory = await _context.Factories.FindAsync(id);
            if (factory == null)
            {
                throw new Exception($"Internal server error.");
            }

            return factory;
        }

        public async Task<NewFactory> AddFactoryAsync(NewFactoryDto dto)
        {
            var entityIns = new NewFactory
            {
                Name = dto.Name,
                Address = dto.Address,
                ContactNumber = dto.ContactNumber,
                Country = dto.Country,
                LedgerId = dto.LedgerId,
            };
            await _context.Factories.AddAsync(entityIns);
            await _context.SaveChangesAsync();
            return entityIns;
        }
    }
}