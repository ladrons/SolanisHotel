using Microsoft.EntityFrameworkCore;
using SolanisHotel.DAL.Context;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.DAL.Repositories.Concretes
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly SolanisHotelContext _context;

        public BaseRepository(SolanisHotelContext context)
        {
            _context = context;
        }
        private DbSet<T> Table => _context.Set<T>();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        //----------//

        public void Add(T item) => Table.Add(item);
        public async Task AddAsync(T item) => await Table.AddAsync(item);
        public async Task AddRangeAsync(List<T> list) => await Table.AddRangeAsync(list);

        public async Task Update(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Modified;
            item.ModifiedDate = DateTime.Now;

            T original = await FindAsync(item.Id);
            Table.Entry(original).CurrentValues.SetValues(item);
        }
        public async Task UpdateRange(List<T> list)
        {
            foreach (T item in list) await Update(item);
        }

        public void Delete(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Deleted;
            item.DeletedDate = DateTime.Now;
        }
        public void DeleteRange(List<T> list)
        {
            foreach (T item in list) Delete(item);
        }

        public void Destroy(T item) => Table.Remove(item);
        public void DestroyRange(List<T> list) => Table.RemoveRange(list);

        //----------//

        public IQueryable<T> GetAll() => Table.AsQueryable();
        public IQueryable<T> GetActives() => Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted);
        public IQueryable<T> GetModifieds() => Where(x => x.Status == ENTITIES.Enums.DataStatus.Modified);
        public IQueryable<T> GetPassives() => Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted);

        //----------//

        public IQueryable<T> Where(Expression<Func<T, bool>> exp) => Table.Where(exp);
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp) => await Table.FirstOrDefaultAsync(exp);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> exp) => Table.AnyAsync(exp);
        public IQueryable<X> Select<X>(Expression<Func<T, X>> exp) => Table.Select(exp);

        //----------//

        public void GetFirstData() => Table.OrderBy(x => x.CreatedDate).FirstOrDefault();
        public void GetLastData() => Table.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

        //----------//

        public async Task<T> FindAsync(int id) => await Table.FindAsync(id);
    }
}