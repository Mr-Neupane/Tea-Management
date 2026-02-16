using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Enums;
using TeaManagement.Interface;
using TeaManagement.Providers;


namespace TeaManagement.Services
{
    public class FactoryService : IFactoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IdProvider  _idProvider;

        public FactoryService(ApplicationDbContext context, IdProvider idProvider)
        {
            _context = context;
            _idProvider = idProvider;
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

        public async Task DeactivateFactoryAsync(int factoryId)
        {
            var fac = await _context.Factories.FindAsync(factoryId);
            if (fac == null)
            {
                throw new Exception($"Internal server error.");
            }
            else
            {
                fac.Status = (int)Status.Inactive;
                await _context.SaveChangesAsync();
            }
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