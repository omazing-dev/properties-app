using MongoDB.Bson;
using MongoDB.Driver;
using PropertyApp.Domain.Entities;
using PropertyApp.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Infraestructure.Seed
{
    public class DataSeeder
    {
        public class DatabaseSeeder
        {
            private readonly MongoDbContext _context;
            private readonly Random _random = new Random();

            public DatabaseSeeder(MongoDbContext context)
            {
                _context = context;
            }

            public void Seed()
            {
                SeedOwners();
                SeedProperties();
                SeedPropertyImages();
                SeedPropertyTraces();
            }

            private void SeedOwners()
            {
                if (_context.Owners.CountDocuments(FilterDefinition<Owner>.Empty) > 0)
                    return;

                var owners = new List<Owner>();
                for (int i = 1; i <= 15; i++)
                {
                    owners.Add(new Owner
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Name = $"Owner {i}",
                        Address = $"Calle {i * 7} # {i}-0{i}",
                        Photo = $"https://randomuser.me/api/portraits/{(i % 2 == 0 ? "men" : "women")}/{10 + i}.jpg",
                        Birthday = DateTime.UtcNow.AddYears(-25 - i).AddDays(i * 3)
                    });
                }

                _context.Owners.InsertMany(owners);
            }

            private void SeedProperties()
            {
                if (_context.Properties.CountDocuments(FilterDefinition<PropertyE>.Empty) > 0)
                    return;

                var owners = _context.Owners.Find(FilterDefinition<Owner>.Empty).ToList();
                if (owners.Count == 0)
                {
                    SeedOwners();
                    owners = _context.Owners.Find(FilterDefinition<Owner>.Empty).ToList();
                }

                var properties = new List<PropertyE>();
                for (int i = 1; i <= 15; i++)
                {
                    var owner = owners[_random.Next(owners.Count)];
                    properties.Add(new PropertyE
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Name = $"Propiedad {i}",
                        Address = $"Av. Principal {i * 5}, Ciudad {((i % 5) + 1)}",
                        Price = (decimal)_random.Next(80_000, 500_000),
                        CodeInternal = $"PR-{i:000}",
                        Year = _random.Next(1995, 2025),
                        IdOwner = owner.Id
                    });
                }

                _context.Properties.InsertMany(properties);
            }

            private void SeedPropertyImages()
            {
                if (_context.PropertyImages.CountDocuments(FilterDefinition<PropertyImage>.Empty) > 0)
                    return;

                var properties = _context.Properties.Find(FilterDefinition<PropertyE>.Empty).ToList();
                if (properties.Count == 0)
                {
                    SeedProperties();
                    properties = _context.Properties.Find(FilterDefinition<PropertyE>.Empty).ToList();
                }

                var imageUrls = new[]
                {
                "https://images.unsplash.com/photo-1570129477492-45c003edd2be",
                "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                "https://images.unsplash.com/photo-1599427303058-f04cbcf4756f",
                "https://images.unsplash.com/photo-1572120360610-d971b9b78825",
                "https://images.unsplash.com/photo-1600585153837-878f3aeea7a2",
                "https://images.unsplash.com/photo-1560185127-6ed189bf02f4",
                "https://images.unsplash.com/photo-1600607686984-cf57fba7f4f5",
                "https://images.unsplash.com/photo-1600585152571-664aa80a1219",
                "https://images.unsplash.com/photo-1600585152591-d5a5d4d5d4d4",
                "https://images.unsplash.com/photo-1560184897-6e4f6d7c44a3",
                "https://images.unsplash.com/photo-1600585152349-39cf6b31d6a9",
                "https://images.unsplash.com/photo-1600585152565-d0c5a71a3a0c",
                "https://images.unsplash.com/photo-1600585152224-d5a5d4d5d4d4",
                "https://images.unsplash.com/photo-1501183638710-841dd1904471",
                "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85"
            };

                string WithParams(string url) => $"{url}?auto=format&fit=crop&w=1200&q=60";

                var propertyImages = new List<PropertyImage>();
                for (int i = 0; i < 15; i++)
                {
                    var prop = properties[i % properties.Count];
                    propertyImages.Add(new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        IdProperty = prop.Id,
                        File = WithParams(imageUrls[i % imageUrls.Length]),
                        Enabled = true
                    });
                }

                _context.PropertyImages.InsertMany(propertyImages);
            }

            private void SeedPropertyTraces()
            {
                if (_context.PropertyTraces.CountDocuments(FilterDefinition<PropertyTrace>.Empty) > 0)
                    return;

                var properties = _context.Properties.Find(FilterDefinition<PropertyE>.Empty).ToList();
                if (properties.Count == 0)
                {
                    SeedProperties();
                    properties = _context.Properties.Find(FilterDefinition<PropertyE>.Empty).ToList();
                }

                var traces = new List<PropertyTrace>();
                for (int i = 1; i <= 15; i++)
                {
                    var prop = properties[_random.Next(properties.Count)];
                    var value = (decimal)_random.Next(90_000, 550_000);
                    var tax = Math.Round(value * 0.09m, 2);

                    traces.Add(new PropertyTrace
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        IdProperty = prop.Id,
                        DateSale = DateTime.UtcNow.AddDays(-_random.Next(10, 1000)),
                        Name = $"Transacción {i} - {prop.Name}",
                        Value = value,
                        Tax = tax
                    });
                }

                _context.PropertyTraces.InsertMany(traces);
            }
        }
    }
}
