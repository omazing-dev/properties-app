using MongoDB.Driver;
using PropertyApp.Application.Interfaces;
using PropertyApp.Domain.Entities;
using PropertyApp.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Infraestructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<PropertyE> _collection;
        public PropertyRepository(MongoDbContext context)
        {
            _collection = context.Properties;
        }
        public async Task<(IEnumerable<PropertyE> Properties, int Total)> GetFilteredAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            var filterBuilder = Builders<PropertyE>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
                filter &= filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));

            if (!string.IsNullOrEmpty(address))
                filter &= filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i"));

            if (minPrice.HasValue)
                filter &= filterBuilder.Gte(p => p.Price, minPrice.Value);

            if (maxPrice.HasValue)
                filter &= filterBuilder.Lte(p => p.Price, maxPrice.Value);

            var total = (int)await _collection.CountDocumentsAsync(filter);

            var properties = await _collection
                .Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (properties, total);
        }
    }
}
