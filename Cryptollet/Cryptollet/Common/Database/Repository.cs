using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptollet.Common.Extensions;
using Microsoft.AppCenter.Crashes;
using SQLite;

namespace Cryptollet.Common.Database
{
    public interface IDatabaseItem
    {
        int Id { get; set; }
    }

    public abstract class BaseDatabaseItem : IDatabaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }

    public interface IRepository<T> where T : IDatabaseItem, new()
    {
        Task<T> GetById(int id);
        Task<int> DeleteAsync(T item);
        Task<List<T>> GetAllAsync();
        Task<int> SaveAsync(T item);
        SQLiteAsyncConnection Database { get; }
    }

    public class Repository<T> : IRepository<T> where T : IDatabaseItem, new()
    {
        readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
        });

        public SQLiteAsyncConnection Database => lazyInitializer.Value;

        public Repository() => InitializeAsync().SafeFireAndForget(false);

        async Task InitializeAsync()
        {
            if (Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
            {
                return;
            }
            await Database.CreateTableAsync(typeof(T)).ConfigureAwait(false);
        }

        public Task<T> GetById(int id) => Database.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();

        public Task<int> DeleteAsync(T item) => Database.DeleteAsync(item);

        public Task<List<T>> GetAllAsync() => Database.Table<T>().ToListAsync();

        public Task<int> SaveAsync(T item) => item.Id != 0 ? Database.UpdateAsync(item) : Database.InsertAsync(item);
    }
}