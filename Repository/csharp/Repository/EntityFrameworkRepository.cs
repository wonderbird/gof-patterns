using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public sealed class EntityFrameworkRepository : IRepository, IDisposable
{
    private readonly ExercisesDbContext _context;

    public EntityFrameworkRepository(string connectionString) =>
        _context = new ExercisesDbContext(connectionString);

    public async Task<IEnumerable<Exercise>> ListExercises() => await _context.Exercises.Select(dto => new Exercise(dto)).ToListAsync();

    public async Task Add(Exercise exercise)
    {
        var exerciseDto = new ExerciseDto(exercise);

        _context.Exercises.Add(exerciseDto);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration) => await _context.Exercises.Select(dto => new Exercise(dto)).ToListAsync();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

public class ExerciseDto
{
    public int Id { get; set; }

    public DateTime Start { get; set; }

    public ExerciseDto()
    {
        Id = 0;
        Start = DateTime.Now.ToUniversalTime();
    }

    public ExerciseDto(Exercise exercise)
    {
        if (string.IsNullOrEmpty(exercise.Id))
        {
            Id = 0;
        }
        else
        {
            Id = int.Parse(exercise.Id, CultureInfo.InvariantCulture);
        }
        Start = exercise.Start.ToUniversalTime();
    }
}

internal class ExercisesDbContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<ExerciseDto> Exercises { get; set; }

    public ExercisesDbContext(string connectionString) => _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_connectionString);
}
