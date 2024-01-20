using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Manager.Domain.Entities;
namespace Manager.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<T> Remove(long id);
        Task<T> GetById(long id);
        Task<List<T>> GetAll();

    }
}
