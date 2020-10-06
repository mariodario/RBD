using System.Collections.Generic;

namespace PrisonDatabase.MongoDbModel
{
    public class PrisonBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Prisoner> Prisoners { get; set; }
    }
}
