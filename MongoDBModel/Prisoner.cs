using PrisonDatabase.Utils;
using System.Collections.Generic;

namespace PrisonDatabase.MongoDbModel
{
    public class Prisoner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public CrimeType CrimeType { get; set; }
        public List<PrisonerAction> PrisonerActions { get; set; }
    }
}