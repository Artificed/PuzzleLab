using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;
using PuzzleLab.Infrastructure.Persistence.Utils;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuestionSeeder(DatabaseContext databaseContext)
{
    private readonly QuestionRepository _questionRepository = new(databaseContext);
    private readonly QuestionPackageRepository _packageRepository = new(databaseContext);
    private readonly QuestionFactory _questionFactory = new();

    public async Task SeedQuestionsAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Questions...");

        if ((await _questionRepository.GetAllQuestionsAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Questions already seeded.");
            return;
        }

        var packages = await _packageRepository.GetAllQuestionPackagesAsync(cancellationToken);
        if (!packages.Any())
        {
            Console.WriteLine("No Question Packages found. Aborting Question seeding.");
            return;
        }

        var packageIdCycle = packages.Select(p => p.Id).ToList();

        var questionsToSeed = new List<(string Text, bool UseImage)>
        {
            ("What is 2 + 2?", false),
            ("What is the capital of Spain?", false),
            ("Who wrote 'Hamlet'?", false),
            ("What element has the symbol 'O'?", false),
            ("Identify the largest planet in our solar system.", true),
            ("What is the chemical formula for water?", false),
            ("In which year did World War II end?", false),
            ("What is the square root of 144?", false),
            ("Name the longest river in the world.", false),
            ("Which country is known as the Land of the Rising Sun?", true)
        };

        var count = 0;
        for (var i = 0; i < questionsToSeed.Count; i++)
        {
            var (text, useImage) = questionsToSeed[i];
            var packageId = packageIdCycle[i % packageIdCycle.Count];

            Question question;
            if (useImage)
            {
                var imageRelativePath = "Persistence/Seeders/Images/webdev-quiz.jpg";

                byte[] seedImageData;
                try
                {
                    seedImageData = SeedHelper.LoadImageDataFromFile(imageRelativePath);
                    if (seedImageData == null || seedImageData.Length == 0)
                    {
                        throw new InvalidOperationException(
                            $"Failed to load seed image data from '{imageRelativePath}'. Check file existence and build properties.");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Error loading seed image data from '{imageRelativePath}'. See inner exception.", ex);
                }

                string mimeType = "image/jpg";

                question = _questionFactory.CreateQuestionWithImage(packageId, text, seedImageData, mimeType);
            }
            else
            {
                question = _questionFactory.CreateQuestion(packageId, text);
            }

            await _questionRepository.InsertQuestionAsync(question, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Questions.");
    }
}