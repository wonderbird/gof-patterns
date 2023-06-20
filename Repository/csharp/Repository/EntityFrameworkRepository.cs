using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EntityFrameworkRepository : IRepository, IDisposable
{
    private readonly ExercisesDbContext _context;

    public EntityFrameworkRepository(string connectionString) =>
        _context = new ExercisesDbContext(connectionString);

    public async Task<IEnumerable<Exercise>> ListExercises() => await _context.Exercises.ToListAsync();

    public async Task Add(Exercise exercise) => throw new NotImplementedException();

    public async Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration) => throw new NotImplementedException();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

internal class ExercisesDbContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Exercise> Exercises { get; set; }

    public ExercisesDbContext(string connectionString) => _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_connectionString);
}
