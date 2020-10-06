using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using static PrisonDatabase.Program;
using Equipment = PrisonDatabase.Model.Equipment;
using Prison = PrisonDatabase.Model.Prison;
using PrisonBlock = PrisonDatabase.Model.PrisonBlock;
using Prisoner = PrisonDatabase.Model.Prisoner;
using PrisonerAction = PrisonDatabase.Model.PrisonerAction;

namespace PrisonDatabase.Utils
{
    public static class InsertData
    {
        public static void SQL(List<object> sqlData)
        {
            var databaseContext = new DatabaseContext();

            databaseContext.Prisons.AddRange((List<Prison>)sqlData[0]);
            databaseContext.Equipment.AddRange((List<Equipment>)sqlData[1]);
            databaseContext.PrisonBlocks.AddRange((List<PrisonBlock>)sqlData[2]);
            databaseContext.Prisoners.AddRange((List<Prisoner>)sqlData[3]);
            databaseContext.PrisonerActions.AddRange((List<PrisonerAction>)sqlData[4]);

            databaseContext.SaveChanges();
        }

        public static void MongoDB(List<MongoDbModel.Prison> prisons)
        {
            MongoContext.collection.InsertMany(prisons);
        }
    }

    public static class DeleteData
    {
        public static void SQL()
        {
            var databaseContext = new DatabaseContext();

            databaseContext.Prisons.RemoveRange(databaseContext.Prisons.ToList());
            databaseContext.Equipment.RemoveRange(databaseContext.Equipment.ToList());
            databaseContext.PrisonBlocks.RemoveRange(databaseContext.PrisonBlocks.ToList());
            databaseContext.Prisoners.RemoveRange(databaseContext.Prisoners.ToList());
            databaseContext.PrisonerActions.RemoveRange(databaseContext.PrisonerActions.ToList());

            databaseContext.SaveChanges();
        }

        public static void MongoDB()
        {
            MongoContext.database.DropCollection("Prisons");
            MongoContext.database.CreateCollection("Prisons");
        }
    }
}
