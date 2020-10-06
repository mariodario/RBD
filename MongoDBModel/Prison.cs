using System.Collections.Generic;

namespace PrisonDatabase.MongoDbModel
{
    public class Prison
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<PrisonBlock> PrisonBlocks { get; set; }
        public List<Equipment> Equipments { get; set; }

    }
}
