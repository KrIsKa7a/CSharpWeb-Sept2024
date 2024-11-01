namespace CinemaApp.Data.Repository
{
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    using Interfaces;

    public class BaseRepository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly CinemaDbContext dbContext;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TType>();
        }

        public TType GetById(TId id)
        {
            TType entity = this.dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet
                .FindAsync(id);

            return entity;
        }

        public TType FirstOrDefault(Func<TType, bool> predicate)
        {
            TType entity = this.dbSet
                .FirstOrDefault(predicate);

            return entity;
        }

        public async Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate)
        {
            TType entity = await this.dbSet
                .FirstOrDefaultAsync(predicate);

            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public void Add(TType item)
        {
            this.dbSet.Add(item);
            this.dbContext.SaveChanges();
        }

        public async Task AddAsync(TType item)
        {
            await this.dbSet.AddAsync(item);
            await this.dbContext.SaveChangesAsync();
        }

        public void AddRange(TType[] items)
        {
            this.dbSet.AddRange(items);
            this.dbContext.SaveChanges();
        }

        public async Task AddRangeAsync(TType[] items)
        {
            await this.dbSet.AddRangeAsync(items);
            await this.dbContext.SaveChangesAsync();
        }

        public bool Delete(TType entity)
        {
            this.dbSet.Remove(entity);
            this.dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAsync(TType entity)
        {
            this.dbSet.Remove(entity);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public bool Update(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.dbContext.Entry(item).State = EntityState.Modified;
                this.dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.dbContext.Entry(item).State = EntityState.Modified;
                await this.dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
