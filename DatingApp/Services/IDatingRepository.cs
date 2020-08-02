using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Models;

namespace DatingApp.Services
{
    public interface IDatingRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Delete(T entity);
        Task<T> GetById(int id);
        Task<ICollection<T>> GetAll();
        Task<bool> SaveAll();
    }
}