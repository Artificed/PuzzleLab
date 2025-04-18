using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizAnswerSeeder(
    IQuizAnswerRepository quizAnswerRepository,
    QuizAnswerFactory quizAnswerFactory,
    IQuizSessionRepository quizSessionRepository,
    IQuestionRepository questionRepository,
    IAnswerRepository answerRepository,
    IQuizRepository quizRepository)
{
    public async Task SeedQuizAnswersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quiz Answers...");

        if ((await quizAnswerRepository.GetAllQuizAnswersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quiz Answers already seeded.");
            return;
        }

        var sessions = await quizSessionRepository.GetAllQuizSessionsAsync(cancellationToken);
        if (!sessions.Any())
        {
            Console.WriteLine("No Quiz Sessions found. Aborting Quiz Answer seeding.");
            return;
        }

        var quizzes = (await quizRepository.GetAllQuizzesAsync(cancellationToken))
            .ToDictionary(q => q.Id); // Faster lookup

        var allQuestions = await questionRepository.GetAllQuestionsAsync(cancellationToken);
        var questionsByPackage = allQuestions.GroupBy(q => q.QuestionPackageId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var allAnswers = await answerRepository.GetAllAnswersAsync(cancellationToken);
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

                var quizAnswer = quizAnswerFactory.CreateQuizAnswer(
                    session.Id,
                    question.Id,
                    selectedAnswer.Id,
                    selectedAnswer.IsCorrect
                );

                await quizAnswerRepository.InsertQuizAnswerAsync(quizAnswer, cancellationToken);
                count++;
            }
        }

        Console.WriteLine($"Seeded {count} Quiz Answers.");
    }
}