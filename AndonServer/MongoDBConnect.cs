using System.Security.Authentication;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Diagnostics;

namespace AndonServer
{
     class MongoDBConnect
    {
        readonly static string databaseName = "AndonDB";
        readonly static string collectionName = "ClientCollection";
        readonly static  string connectionString = Helper.ConnectionString;
       
        public static async void SendToDB(List<Models.ClientData> UploadList)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(databaseName);
            var collection = db.GetCollection<Models.ClientData>(collectionName);


            foreach (Models.ClientData Client in UploadList)
            {
               
                await collection.InsertOneAsync(Client);
            }
            var results = await collection.FindAsync(_ => true);

            foreach (var result in results.ToList()) 
            {
                Debug.WriteLine($"{result.ComputerName} -  {result.ColorCode}");
            }
        }
    
    }
}
