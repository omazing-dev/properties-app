using MongoDB.Driver;
using PropertyApp.Application.Interfaces;
using PropertyApp.Domain.Entities;
using PropertyApp.Infraestructure.Context;

namespace PropertyApp.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<PropertyE> _properties;
        private readonly IMongoCollection<PropertyImage> _images;

        public PropertyRepository(MongoDbContext context)
        {
            _properties = context.Properties;
            _images = context.PropertyImages;
        }

        public async Task<(IEnumerable<(PropertyE Property, string? Image)>, int Total)> GetFilteredAsync(
            string? name,
            string? address,
            decimal? minPrice,
            decimal? maxPrice,
            int page,
            int pageSize)
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

            var total = (int)await _properties.CountDocumentsAsync(filter);

            var properties = await _properties
                .Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var result = new List<(PropertyE, string?)>();

            foreach (var prop in properties)
            {
                var image = await _images
                    .Find(i => i.IdProperty == prop.Id && i.Enabled)
                    .FirstOrDefaultAsync();

                result.Add((prop, image?.File));
            }

            return (result, total);
        }
    }
}
