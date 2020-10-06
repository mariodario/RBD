using System.Collections.Generic;

namespace PrisonDatabase.Model
{
    public class Prison
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public virtual ICollection<PrisonBlock> PrisonBlocks { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
