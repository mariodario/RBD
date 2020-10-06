namespace PrisonDatabase.Model
{
    public class PrisonerAction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Value { get; set; }
        public Prisoner Prisoner { get; set; }
    }
}