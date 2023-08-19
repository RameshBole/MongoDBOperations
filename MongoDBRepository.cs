using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBApp
{
    public class MongoDBRepository : IMongoDBRepository
    {
        public MongoDBRepository()
        {
                
        }
        public MongoClient connectDataBase()
        {
            return  new MongoClient("mongodb://127.0.0.1:27017");
        }

      public BsonDocument GetDatabaseStats(MongoClient dbClient, string databaseName)
        {
            BsonDocument databaseStats = new BsonDocument();
            if (dbClient != null)
            {
                IMongoDatabase db = dbClient.GetDatabase(databaseName);
                var command = new BsonDocument { { "dbstats", 1 } };
                databaseStats = db.RunCommand<BsonDocument>(command);
            }
            return databaseStats;
        }

        public IMongoDatabase GetDatabase(MongoClient dbClient, string databaseName)
        {
              return  dbClient.GetDatabase(databaseName);
                
        }

        public List<BsonDocument> InsertCollection(MongoClient dbClient, string databaseName)
        {
            var connectedDatabase = GetDatabase(dbClient, databaseName);
            var collection = connectedDatabase.GetCollection<BsonDocument>(databaseName);

            List<BsonDocument> objList = new List<BsonDocument>();
            for (int i = 0; i < 100; i++)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("name", "Ramesh" + i);

                var doc = collection.Find(filter).FirstOrDefault();
                if (doc == null)
                {
                    objList.Add(new BsonDocument {
                                { "name", "Ramesh"+ i },
                                { "Type", "Software" +i },
                                { "Email", "bole.ramesh"+i+"@gmail.com" },
                                { "info", new BsonDocument {
                                    { "Address", "guntur"+i }, { "colony", "colony"+i }
                                }
                                }
                            });
                }

            }
            if (objList.Count > 0) { collection.InsertMany(objList); }

            return objList;

        }
        public List<BsonDocument> GetDatabases(MongoClient dbClient)
        {
            List<BsonDocument> databases = new List<BsonDocument>();
           if(dbClient != null)
            {
                dbClient.ListDatabases().ToList().ForEach(db => databases.Add(db));
            }
           return databases;
        }
    }
}
