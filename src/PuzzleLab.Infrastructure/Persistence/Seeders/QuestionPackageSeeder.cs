using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuestionPackageSeeder(DatabaseContext databaseContext)
{
    private readonly QuestionPackageRepository _packageRepository = new(databaseContext);
    private readonly QuestionPackageFactory _packageFactory = new();

    public async Task SeedQuestionPackagesAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Question Packages...");

        if ((await _packageRepository.GetAllQuestionPackagesAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Question Packages already seeded.");
            return;
        }

        var packagesToSeed = new List<(string Name, string Description)>
        {
            ("General Knowledge Basics", "A mix of simple general knowledge questions."),
            ("European Geography", "Questions about countries and capitals in Europe."),
            ("Basic Algebra", "Introductory algebraic concepts."),
            ("World History Highlights", "Key events from world history."),
            ("Science Fundamentals", "Basic concepts from physics, chemistry, and biology."),
            ("Pop Culture Trivia", "Questions about recent movies, music, and trends.")
        };

        int count = 0;
        foreach (var (name, description) in packagesToSeed)
        {
            var package = _packageFactory.CreateQuestionPackage(name, description);
            await _packageRepository.InsertQuestionPackageAsync(package, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Question Packages.");
    }
}