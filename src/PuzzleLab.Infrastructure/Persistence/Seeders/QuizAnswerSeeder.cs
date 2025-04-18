using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizAnswerSeeder(DatabaseContext databaseContext)
{
    private readonly QuizAnswerRepository _quizAnswerRepository = new(databaseContext);
    private readonly QuizAnswerFactory _quizAnswerFactory = new();

    private readonly QuizSessionRepository _quizSessionRepository = new(databaseContext);
    private readonly QuestionRepository _questionRepository = new(databaseContext);
    private readonly AnswerRepository _answerRepository = new(databaseContext);
    private readonly QuizRepository _quizRepository = new(databaseContext);

    public async Task SeedQuizAnswersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quiz Answers...");

        if ((await _quizAnswerRepository.GetAllQuizAnswersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quiz Answers already seeded.");
            return;
        }

        var sessions = await _quizSessionRepository.GetAllQuizSessionsAsync(cancellationToken);
        if (!sessions.Any())
        {
            Console.WriteLine("No Quiz Sessions found. Aborting Quiz Answer seeding.");
            return;
        }

        var quizzes = (await _quizRepository.GetAllQuizzesAsync(cancellationToken))
            .ToDictionary(q => q.Id); // Faster lookup

        var allQuestions = await _questionRepository.GetAllQuestionsAsync(cancellationToken);
        var questionsByPackage = allQuestions.GroupBy(q => q.QuestionPackageId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var allAnswers = await _answerRepository.GetAllAnswersAsync(cancellationToken);
        var answersByQuestion = allAnswers.GroupBy(a => a.QuestionId)
            .ToDictionary(g => g.Key, g => g.ToList());

        int count = 0;
        var random = new Random();

        foreach (var session in sessions)
        {
            if (!quizzes.TryGetValue(session.QuizId, out var quiz))
            {
                Console.WriteLine(
                    $"Warning: Could not find Quiz {session.QuizId} for Session {session.Id}. Skipping answers for this session.");
                continue;
            }

            if (!questionsByPackage.TryGetValue(quiz.QuestionPackageId, out var questionsForQuiz))
            {
                Console.WriteLine(
                    $"Warning: Could not find questions for Package {quiz.QuestionPackageId} (Quiz {quiz.Id}, Session {session.Id}). Skipping answers.");
                continue;
            }

            foreach (var question in questionsForQuiz)
            {
                if (!answersByQuestion.TryGetValue(question.Id, out var possibleAnswers))
                {
                    Console.WriteLine(
                        $"Warning: No answers found for Question {question.Id}. Cannot seed QuizAnswer for Session {session.Id}.");
                    continue;
                }

                if (!possibleAnswers.Any())
                {
                    Console.WriteLine(
                        $"Warning: Question {question.Id} has an empty answer list. Cannot seed QuizAnswer for Session {session.Id}.");
                    continue;
                }

                var selectedAnswer = possibleAnswers[random.Next(possibleAnswers.Count)];

                var quizAnswer = _quizAnswerFactory.CreateQuizAnswer(
                    session.Id,
                    question.Id,
                    selectedAnswer.Id,
                    selectedAnswer.IsCorrect
                );

                await _quizAnswerRepository.InsertQuizAnswerAsync(quizAnswer, cancellationToken);
                count++;
            }
        }

        Console.WriteLine($"Seeded {count} Quiz Answers.");
    }
}