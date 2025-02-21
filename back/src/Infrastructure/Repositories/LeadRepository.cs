using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Persistence;

namespace MyApp.Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;

      
        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }

       

        public async Task<Lead> GetByIdAsync(Guid id)
        {
            return await _context.Leads.FindAsync(id);
        }

        public async Task<List<Lead>> GetAllAsync()
        {
            return await _context.Leads.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Lead lead)
        {
            await _context.Leads.AddAsync(lead);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lead lead)
        {
            _context.Leads.Update(lead);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead != null)
            {
                _context.Leads.Remove(lead);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status)
        {
            return await _context.Leads.Where(l => l.Status == status).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
