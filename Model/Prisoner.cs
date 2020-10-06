using PrisonDatabase.Utils;
using System.Collections.Generic;

namespace PrisonDatabase.Model
{
    public class Prisoner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public CrimeType CrimeType { get; set; }
        public PrisonBlock PrisonBlock { get; set; }
        public virtual ICollection<PrisonerAction> PrisonerActions { get; set; }
    }
}