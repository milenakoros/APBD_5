using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Klinika;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    // GET: api/Animals
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        return DataStore.Animals;
    }

    // GET: api/Animals/5
    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimal(int id)
    {
        var animal = DataStore.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound();
        }
        return animal;
    }

    // POST: api/Animals
    [HttpPost]
    public ActionResult<Animal> PostAnimal(Animal animal)
    {
        DataStore.Animals.Add(animal);
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    // PUT: api/Animals/5
    [HttpPut("{id}")]
    public IActionResult PutAnimal(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return BadRequest();
        }

        var existingAnimal = DataStore.Animals.FirstOrDefault(a => a.Id == id);
        if (existingAnimal == null)
        {
            return NotFound();
        }

        existingAnimal.Name = animal.Name;
        existingAnimal.Category = animal.Category;
        existingAnimal.Weight = animal.Weight;
        existingAnimal.FurColor = animal.FurColor;

        return NoContent();
    }

    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = DataStore.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound();
        }

        DataStore.Animals.Remove(animal);

        return NoContent();
    }
    private readonly IConfiguration _configuration;

    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection GetSqlConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    // GET: api/Animals
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals(string orderBy = "name")
    {
        var animals = new List<Animal>();

        using (var connection = GetSqlConnection())
        {
            connection.Open();
            var query = $"SELECT * FROM Animals ORDER BY {orderBy}";
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        animals.Add(new Animal
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Category = reader.GetString(2),
                            Weight = reader.GetDouble(3),
                            FurColor = reader.GetString(4)
                        });
                    }
                }
            }
        }

        return animals;
    }
}

