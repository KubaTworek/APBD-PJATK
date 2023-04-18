namespace Kolokwium.Model
{
    public class Medicament
    {
        public int MedicamentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Medicament(int medicamentId, string name, string description, string type)
        {
            MedicamentId = medicamentId;
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
