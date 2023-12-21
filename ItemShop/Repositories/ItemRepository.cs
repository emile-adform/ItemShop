using Dapper;
using ItemShop.Interfaces;
using ItemShop.Models.DTOs;
using ItemShop.Models.Entities;
using System.Data;

namespace ItemShop.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDbConnection _connection;
        public ItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task Create(Item item)
        {
            string sql = $"INSERT INTO items (name, price) VALUES (@Name, @Price)";
            var queryArguments = new
            {
                Name = item.Name,
                Price = item.Price
            };
            await _connection.QuerySingleAsync<int>(sql, queryArguments);
        }

        public async Task Delete(int id)
        {
            string sql = $"DELETE FROM items WHERE id = @Id";
            await _connection.ExecuteAsync(sql);
        }

        public async Task<List<Item>> Get()
        {
            string sql = $"SELECT * FROM items";
            return (await _connection.QueryAsync<Item>(sql)).ToList();
        }

        public async Task <Item> Get(int id)
        {
            string sql = $"SELECT * FROM items WHERE id = @Id";
            return await _connection.QuerySingleAsync<Item>(sql, new {id});
        }

        public async Task Update(Item item)
        {
            string sql = $"UPDATE items SET name = @Name, price = @Price WHERE id = @Id";
            var queryArguments = new
            {
                Name = item.Name,
                Price = item.Price,
                Id = item.Id
            };
            _connection.ExecuteAsync(sql, queryArguments);
        }
    }
}
