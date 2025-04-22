using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class AnswerSeeder(
    IAnswerRepository answerRepository,
    IQuestionRepository questionRepository,
    AnswerFactory answerFactory)
{
    public async Task SeedAnswersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Answers...");

        if ((await answerRepository.GetAllAnswersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Answers already seeded.");
            return;
        }

        var questions = await questionRepository.GetAllQuestionsAsync(cancellationToken);
        if (!questions.Any())
        {
            Console.WriteLine("No Questions found. Aborting Answer seeding.");
            return;
        }

        int count = 0;
        foreach (var question in questions)
        {
            var answersForQuestion = GetSampleAnswers(question.Text);

            foreach (var (text, isCorrect) in answersForQuestion.Take(4)) // Ensure only 4 answers
            {
                var answer = answerFactory.CreateAnswer(question.Id, text, isCorrect);
                await answerRepository.InsertAnswerAsync(answer, cancellationToken);
                count++;
            }
        }

        Console.WriteLine($"Seeded {count} Answers.");
    }

    private List<(string Text, bool IsCorrect)> GetSampleAnswers(string questionText)
    {
        if (questionText.Contains("2 + 2"))
            return new() { ("3", false), ("4", true), ("5", false), ("22", false) };
        if (questionText.Contains("capital of Spain"))
            return new() { ("Lisbon", false), ("Paris", false), ("Madrid", true), ("Rome", false) };
        if (questionText.Contains("Hamlet"))
            return new() { ("Charles Dickens", false), ("William Shakespeare", true), ("Jane Austen", false), ("Mark Twain", false) };
        if (questionText.Contains("symbol 'O'"))
            return new() { ("Gold", false), ("Oxygen", true), ("Osmium", false), ("Oganesson", false) };
        if (questionText.Contains("largest planet"))
            return new() { ("Earth", false), ("Mars", false), ("Jupiter", true), ("Saturn", false) };
        if (questionText.Contains("formula for water"))
            return new() { ("CO2", false), ("H2O", true), ("NaCl", false), ("O2", false) };
        if (questionText.Contains("World War II"))
            return new() { ("1918", false), ("1939", false), ("1945", true), ("1950", false) };
        if (questionText.Contains("square root of 144"))
            return new() { ("10", false), ("11", false), ("12", true), ("13", false) };
        if (questionText.Contains("longest river"))
            return new() { ("Amazon", true), ("Nile", false), ("Yangtze", false), ("Mississippi", false) };
        if (questionText.Contains("Land of the Rising Sun"))
            return new() { ("China", false), ("Japan", true), ("Thailand", false), ("South Korea", false) };

        return new() { ("Option A", false), ("Option B", true), ("Option C", false), ("Option D", false) };
    }
}
