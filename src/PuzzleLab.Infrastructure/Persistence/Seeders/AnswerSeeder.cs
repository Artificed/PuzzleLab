using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class AnswerSeeder(DatabaseContext databaseContext)
{
    private readonly AnswerRepository _answerRepository = new(databaseContext);
    private readonly QuestionRepository _questionRepository = new(databaseContext);
    private readonly AnswerFactory _answerFactory = new();

    public async Task SeedAnswersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Answers...");

        if ((await _answerRepository.GetAllAnswersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Answers already seeded.");
            return;
        }

        var questions = await _questionRepository.GetAllQuestionsAsync(cancellationToken);
        if (!questions.Any())
        {
            Console.WriteLine("No Questions found. Aborting Answer seeding.");
            return;
        }

        int count = 0;
        foreach (var question in questions)
        {
            var answersForQuestion = GetSampleAnswers(question.Text, question.Id);

            foreach (var (text, isCorrect) in answersForQuestion)
            {
                var answer = _answerFactory.CreateAnswer(question.Id, text, isCorrect);
                await _answerRepository.InsertAnswerAsync(answer, cancellationToken);
                count++;
            }

            if (count >= 5 && questions.Count > 2)
                break;
        }

        if (count < 5 && questions.Any())
        {
            var firstQuestionId = questions.First().Id;
            while (count < 5)
            {
                var answer = _answerFactory.CreateAnswer(firstQuestionId, $"Placeholder Answer {count + 1}", false);
                await _answerRepository.InsertAnswerAsync(answer, cancellationToken);
                count++;
            }
        }

        Console.WriteLine($"Seeded {count} Answers.");
    }

    private List<(string Text, bool IsCorrect)> GetSampleAnswers(string questionText, Guid questionId)
    {
        if (questionText.Contains("2 + 2")) return new() { ("3", false), ("4", true), ("5", false), ("22", false) };
        if (questionText.Contains("capital of Spain"))
            return new() { ("Lisbon", false), ("Paris", false), ("Madrid", true), ("Rome", false) };
        if (questionText.Contains("Hamlet"))
            return new() { ("Charles Dickens", false), ("William Shakespeare", true), ("Jane Austen", false) };
        if (questionText.Contains("symbol 'O'"))
            return new() { ("Gold", false), ("Oxygen", true), ("Osmium", false), ("Oganesson", false) };
        if (questionText.Contains("largest planet"))
            return new() { ("Earth", false), ("Mars", false), ("Jupiter", true), ("Saturn", false) };
        if (questionText.Contains("formula for water"))
            return new() { ("CO2", false), ("H2O", true), ("NaCl", false), ("O2", false) };

        return new() { ("Option A", false), ("Option B", true), ("Option C", false), ("Option D", false) };
    }
}