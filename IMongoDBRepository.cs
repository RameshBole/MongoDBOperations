using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBApp
{
    interface IMongoDBRepository
    {
        MongoClient connectDataBase();
       List<BsonDocument> GetDatabases(MongoClient dbClient);
        BsonDocument GetDatabaseStats(MongoClient dbClient, string databaseName);
        List<BsonDocument> InsertCollection(MongoClient dbClient, string databaseName);
    }
}
