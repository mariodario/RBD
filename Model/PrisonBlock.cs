using System.Collections.Generic;

namespace PrisonDatabase.Model
{
    public class PrisonBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Prison Prison { get; set; }
        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }
}
