using MI4T.IndexService.BAL.ContentMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI4T.IndexService.BAL.AsyncData
{
   public class GetAsyncData
    {
        public static async Task<int>  getDataFromMongoDB(MongoDBModel md,string mongoUrl,string dbName,string tableName)
        {
            var conString = mongoUrl;

            /// creating MongoClient
            var client = new MongoClient(conString);

            ///Get the database
            var DB = client.GetDatabase(dbName);

            ///Get the collcetion from the DB in which you want to insert the data                
            var collection = DB.GetCollection<MongoDBModel>(tableName);
            var filter = Builders<MongoDBModel>.Filter.Eq("ItemURI", md.ItemURI);
            var result11 = await collection.Find(filter).ToListAsync();

            return result11.Count;
        }
    }
}
