using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PrisonDatabase.MongoDbModel;
using System.Collections.Generic;
using System.Linq;
using static PrisonDatabase.Program;

namespace PrisonDatabase
{
    public class Queries
    {
        private readonly DatabaseContext _databaseContext = new DatabaseContext();
        public void SQLFirstQuery()
        {
            var result = _databaseContext.Prisoners
                .Where(x => x.CrimeType == Utils.CrimeType.Extreme)
                .Select(x => x.PrisonBlock.Prison)
                .GroupBy(x => x.Id)
                .Select(x => new
                {
                    Prison = x,
                    ExtremeDangerousCount = x.Count()
                })
                .OrderByDescending(x => x.ExtremeDangerousCount)
                .ToList();
        }

        public void SQLSecondQuery()
        {
            _databaseContext.Database.CommandTimeout = 240;
            var result = _databaseContext.PrisonerActions
                .Where(x => x.Value == true)
                .Select(x => x.Prisoner.PrisonBlock.Prison)
                .GroupBy(x => x.Id)
                .Select(x => new
                {
                    Prison = x.FirstOrDefault(),
                    GoodActionCount = x.Count()
                })
                .OrderByDescending(x => x.GoodActionCount)
                .ToList();
        }

        public void SQLThirdQuery()
        {
            var result = _databaseContext.Prisoners
                .Select(a => new
                {
                    PrisonerId = a.Id,
                    PrisonerName = a.FirstName,
                    PrisonerSurname = a.Surname,
                    GoodActionCount = a.PrisonerActions.Count(b => b.Value == true),
                    BadActionCount = a.PrisonerActions.Count(b => b.Value == false)
                })
                .OrderByDescending(x => x.GoodActionCount)
                .ToList();
        }

        public void MongoFirstQuery(IMongoQueryable<Prison> collection)
        {
            var result = collection.ToList().Select(x => new
            {
                Prison = x,
                ExtremeDangerousCount = x.PrisonBlocks.
                Select(c => c.Prisoners.Where(p => p.CrimeType
                == Utils.CrimeType.Extreme).Count()).Sum()
            })
                .OrderByDescending(x => x.ExtremeDangerousCount)
                .ToList();
        }

        public void MongoSecondQuery(IMongoQueryable<Prison> collection)
        {
            var result = collection.ToList().Select(x => new
            {
                Prison = x,
                GoodActionCount = x.PrisonBlocks
                .Select(c => c.Prisoners.Select(a => a.PrisonerActions
                .Where(p => p.Value == true).Count()).Sum()).Sum()
            })
                .OrderByDescending(x => x.GoodActionCount)
                .ToList();
        }

        public void MongoThirdQuery(IMongoQueryable<Prison> collection)
        {
            List<Prisoner> prisoners = new List<Prisoner>();
            collection.ToList().ForEach(x => x.PrisonBlocks
            .ForEach(p => prisoners.AddRange(p.Prisoners)));

            var result = prisoners.Select(p => new
            {
                PrisonerId = p.Id,
                PrisonerName = p.FirstName,
                PrisonerSurname = p.Surname,
                GoodActionCount = p.PrisonerActions.Count(a => a.Value == true),
                BadActionCount = p.PrisonerActions.Count(a => a.Value == false)
            })
            .OrderByDescending(x => x.GoodActionCount)
            .ToList();
            var c = result;
        }
    }
}
