namespace AnimalAPI.Model
{
    public class Animal
    {
        public long IdAnimal { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category{ get; set; }
        public string Area { get; set; }

        public Animal(long idAnimal, string name, string description, string category, string area)
        {
            IdAnimal = idAnimal;
            Name = name;
            Description = description;
            Category = category;
            Area = area;
        }

        public override string ToString()
        {
            return $"Id: {IdAnimal}, Name: {Name}, Description: {Description}, Category: {Category}, Area: {Area}";
        }
    }
}
