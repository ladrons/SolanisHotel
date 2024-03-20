using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        protected IRepository<T> _iRep;

        public BaseManager(IRepository<T> iRep)
        {
            _iRep = iRep;
        }

        public Task SaveChangesAsync() => _iRep.SaveChangesAsync();

        //----------//

        public void Add(T item) => _iRep.Add(item);
        public async Task AddAsync(T item) => await _iRep.AddAsync(item);
        public async Task AddRangeAsync(List<T> list) => await _iRep.AddRangeAsync(list);

        public async Task Update(T item) => await _iRep.Update(item);
        public async Task UpdateRange(List<T> list) => await _iRep.UpdateRange(list);

        public void Delete(T item) => _iRep.Delete(item);
        public void DeleteRange(List<T> list) => _iRep.DeleteRange(list);

        public void Destroy(T item) => _iRep.Destroy(item);
        public void DestroyRange(List<T> list) => _iRep.DestroyRange(list);

        //----------//

        public IQueryable<T> GetAll() => _iRep.GetAll();
        public IQueryable<T> GetActives() => _iRep.GetActives();
        public IQueryable<T> GetModifieds() => _iRep.GetModifieds();
        public IQueryable<T> GetPassives() => _iRep.GetPassives();

        //----------//

        public IQueryable<T> Where(Expression<Func<T, bool>> exp) => _iRep.Where(exp);
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp) => await _iRep.FirstOrDefaultAsync(exp);
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> exp) => await _iRep.AnyAsync(exp);
        public IQueryable<X> Select<X>(Expression<Func<T, X>> exp) => _iRep.Select(exp);

        //----------//

        public void GetFirstData() => _iRep.GetFirstData();
        public void GetLastData() => _iRep.GetLastData();

        //----------//

        public Task<T> FindAsync(int id) => _iRep.FindAsync(id);
    }
}