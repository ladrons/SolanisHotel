using SolanisHotel.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IManager<T> where T : class, IEntity
    {
        Task SaveChangesAsync();

        //Modify Commands
        void Add(T item);
        Task AddAsync(T item);
        Task AddRangeAsync(List<T> list);

        Task Update(T item);
        Task UpdateRange(List<T> list);

        void Destroy(T item);
        void DestroyRange(List<T> list);

        void Delete(T item);
        void DeleteRange(List<T> list);


        //List Commands
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();
        IQueryable<T> GetPassives();
        IQueryable<T> GetModifieds();


        //LINQ Commands
        IQueryable<T> Where(Expression<Func<T, bool>> exp);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exp);
        IQueryable<X> Select<X>(Expression<Func<T, X>> exp);


        //First-Last Data Commands
        void GetFirstData();
        void GetLastData();


        //Find Command
        Task<T> FindAsync(int id);
    }
}