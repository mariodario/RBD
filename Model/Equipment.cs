namespace PrisonDatabase.Model
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FreeSlots { get; set; }
        public Prison Prison { get; set; }
    }
}
