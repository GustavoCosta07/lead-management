using MyApp.Domain.Entities;
using MyApp.Domain.Enums;

namespace MyApp.Domain.Interfaces
{
    public interface ILeadRepository
    {
        Task<Lead> GetByIdAsync(Guid id);
        Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status);
        Task AddAsync(Lead lead);
        Task UpdateAsync(Lead lead);
        Task DeleteAsync(Guid id); 
        Task<List<Lead>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
