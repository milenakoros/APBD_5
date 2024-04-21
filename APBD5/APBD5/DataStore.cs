namespace Klinika;
public static class DataStore
{
    public static List<Animal> Animals { get; set; } = new List<Animal>
    {
        new Animal { Id = 1, Name = "Rex", Category = "Dog", Weight = 10.5, FurColor = "Brown" },
        new Animal { Id = 2, Name = "Whiskers", Category = "Cat", Weight = 5.2, FurColor = "Black" }
    };

    public static List<Visit> Visits { get; set; } = new List<Visit>
    {
        new Visit { Id = 1, VisitDate = DateTime.Now.AddDays(-1), Description = "Initial checkup", Cost = 50.0, AnimalId = 1 },
        new Visit { Id = 2, VisitDate = DateTime.Now.AddDays(-2), Description = "Vaccination", Cost = 100.0, AnimalId = 2 }
    };
}
