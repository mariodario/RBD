using MongoDB.Driver;
using PrisonDatabase.Generator;
using PrisonDatabase.Model;
using PrisonDatabase.Utils;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace PrisonDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Generate SQL data

            var dataGenerator = new SQLDataGenerator();
            var sqlData = dataGenerator.GenerateData(1);
            Console.WriteLine("SQL data generated!");

            #endregion

            #region Generate MongoDB data

            var mongoDataGenerator = new MongoDataGenerator();
            var mongoData = mongoDataGenerator.GenerateData(1);
            Console.WriteLine("MongoDB data generated!");

            #endregion

            #region Insert data
            DeleteData.SQL();
            DeleteData.MongoDB();
            InsertData.SQL(sqlData);
            InsertData.MongoDB(mongoData);
            #endregion

            var queries = new Queries();

            #region SQL Queries

            var timerSQLFirstQuery = Stopwatch.StartNew();
            queries.SQLFirstQuery();
            timerSQLFirstQuery.Stop();

            var timerSQLSecondQuery = Stopwatch.StartNew();
            queries.SQLSecondQuery();
            timerSQLSecondQuery.Stop();

            var timerSQLThirdQuery = Stopwatch.StartNew();
            queries.SQLThirdQuery();
            timerSQLThirdQuery.Stop();

            #endregion

            #region MongoDB Queries

            var timerMongoFirstQuery = Stopwatch.StartNew();
            queries.MongoFirstQuery(MongoContext.collection.AsQueryable());
            timerMongoFirstQuery.Stop();

            var timerMongoSecondQuery = Stopwatch.StartNew();
            queries.MongoSecondQuery(MongoContext.collection.AsQueryable());
            timerMongoSecondQuery.Stop();

            var timerMongoThirdQuery = Stopwatch.StartNew();
            queries.MongoThirdQuery(MongoContext.collection.AsQueryable());
            timerMongoThirdQuery.Stop();

            #endregion

            Console.WriteLine("SQL First Query: " + timerSQLFirstQuery.ElapsedMilliseconds);
            Console.WriteLine("SQL Second Query: " + timerSQLSecondQuery.ElapsedMilliseconds);
            Console.WriteLine("SQL Third Query: " + timerSQLThirdQuery.ElapsedMilliseconds);
            Console.WriteLine("Mongo First Query: " + timerMongoFirstQuery.ElapsedMilliseconds);
            Console.WriteLine("Mongo Second Query: " + timerMongoSecondQuery.ElapsedMilliseconds);
            Console.WriteLine("Mongo Third Query: " + timerMongoThirdQuery.ElapsedMilliseconds);
        }
    }

    public static class MongoContext
    {
        private static readonly MongoClientSettings _settings = new MongoClientSettings
        {
            Servers = new[]
            {
                    new MongoServerAddress("mongo0", 30000),
                    new MongoServerAddress("mongo1", 30001),
                    new MongoServerAddress("mongo2", 30002)
                },
            ConnectionMode = ConnectionMode.ReplicaSet,
            ReplicaSetName = "rs0"
        };

        public static IMongoDatabase database = new MongoClient(_settings)
            .GetDatabase("prisonDatabase");

        public static IMongoCollection<MongoDbModel.Prison> collection =
           database.GetCollection<MongoDbModel.Prison>("Prisons");
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Prison> Prisons { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<PrisonBlock> PrisonBlocks { get; set; }
        public DbSet<Prisoner> Prisoners { get; set; }
        public DbSet<PrisonerAction> PrisonerActions { get; set; }

    }
}
