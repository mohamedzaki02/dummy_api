using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Data;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Services
{
    public class DatingRepository<T> : IDatingRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;

        }
        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<ICollection<T>> GetAll()
        {
            var dbSet = _context.Set<T>();
            if (typeof(T) == typeof(User)) return await dbSet.Include("Photos").ToListAsync();
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().Include("Photos").FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}