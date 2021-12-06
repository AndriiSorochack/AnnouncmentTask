using AnnouncmentTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncmentTask.Data
{
    public class AnnouncmentRepository : IRepository<Announcment>
    {
        private TaskContext _context;
        public AnnouncmentRepository (TaskContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task CreateAsync(Announcment entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Announcment> GetAll()
        {
            return _context.Announcment.ToList();
        }

        public async Task<Announcment> GetByIdAsync(int id)
        {
            return await _context.Announcment.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Announcment.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Announcment entity)
        {
            var result = await _context.Announcment
                                       .FirstOrDefaultAsync(a => a.Id == entity.Id);

            if (result != null)
            {
                result.Name = entity.Name;
                result.Description = entity.Description;
                result.DateOfCreation = entity.DateOfCreation;
            }

            await _context.SaveChangesAsync();
        }
    }


}
