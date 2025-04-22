using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Persistence.Utils;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuestionSeeder(
    IQuestionRepository questionRepository,
    IQuestionPackageRepository packageRepository,
    QuestionFactory questionFactory)
{
    public async Task SeedQuestionsAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Questions...");

        if ((await questionRepository.GetAllQuestionsAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Questions already seeded.");
            return;
        }

        var packages = await packageRepository.GetAllQuestionPackagesAsync(cancellationToken);
        if (!packages.Any())
        {
            Console.WriteLine("No Question Packages found. Aborting Question seeding.");
            return;
        }

        var packageIdCycle = packages.Select(p => p.Id).ToList();

        var questionsToSeed = new List<(string Text, string ImageFileName)>
        {
            ("What is 2 + 2?", "2+2.png"),
            ("What is the capital of Spain?", "spain.png"),
            ("Who wrote 'Hamlet'?", "hamlet.jpg"),
            ("What element has the symbol 'O'?", "periodic-table.png"),
            ("Identify the largest planet in our solar system.", "solar-system.png"),
            ("What is the chemical formula for water?", "water.jpg"),
            ("In which year did World War II end?", "ww2.png"),
            ("What is the square root of 144?", "root144.jpg"),
            ("Name the longest river in the world.", "river.jpg"),
            ("Which country is known as the Land of the Rising Sun?", "jp.png")
        };

        var count = 0;
        for (var i = 0; i < questionsToSeed.Count; i++)
        {
            var (text, imageFileName) = questionsToSeed[i];
            var packageId = packageIdCycle[i % packageIdCycle.Count];

            Question question;
            var imageRelativePath = Path.Combine("Persistence", "Seeders", "Images", imageFileName);
            var fileExtension = Path.GetExtension(imageFileName)?.TrimStart('.').ToLowerInvariant();

            if (!string.IsNullOrEmpty(fileExtension))
            {
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

                string mimeType = fileExtension switch
                {
                    "jpg" or "jpeg" => "image/jpeg",
                    "png" => "image/png",
                    "gif" => "image/gif",
                    _ => throw new InvalidOperationException($"Unsupported image format: {fileExtension}")
                };

                question = questionFactory.CreateQuestionWithImage(packageId, text, seedImageData, mimeType);
            }
            else
            {
                question = questionFactory.CreateQuestion(packageId, text);
            }

            await questionRepository.InsertQuestionAsync(question, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Questions.");
    }
}