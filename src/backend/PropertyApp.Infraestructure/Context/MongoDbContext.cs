using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;
using PropertyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Infraestructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Owner> Owners
       => _database.GetCollection<Owner>("Owners");

        public IMongoCollection<PropertyE> Properties
            => _database.GetCollection<PropertyE>("Properties");

        public IMongoCollection<PropertyImage> PropertyImages
            => _database.GetCollection<PropertyImage>("PropertyImages");
        public IMongoCollection<PropertyTrace> PropertyTraces
           => _database.GetCollection<PropertyTrace>("PropertyTraces");

        public void InitializeDatabase()
        {
            var existingCollections = _database.ListCollectionNames().ToList();

            if (!existingCollections.Contains("Owners"))
                _database.CreateCollection("Owners");

            if (!existingCollections.Contains("Properties"))
                _database.CreateCollection("Properties");

            if (!existingCollections.Contains("PropertyImages"))
                _database.CreateCollection("PropertyImages");

            if (!existingCollections.Contains("PropertyTraces"))
                _database.CreateCollection("PropertyTraces");
        }
    }
}
