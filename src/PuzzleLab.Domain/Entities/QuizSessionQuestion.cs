using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class QuizSessionQuestion
{
    [Key] public Guid Id { get; private set; }

    [Required] public Guid QuizSessionId { get; private set; }

    [Required] public Guid QuestionId { get; private set; }

    [Required] public int QuestionOrder { get; private set; }

    public bool Answered { get; private set; }

    [ForeignKey("QuizSessionId")] public virtual QuizSession QuizSession { get; private set; }

    [ForeignKey("QuestionId")] public virtual Question Question { get; private set; }

    private QuizSessionQuestion()
    {
    }

    public QuizSessionQuestion(Guid quizSessionId, Guid questionId, int questionOrder)
    {
        Id = Guid.NewGuid();
        QuizSessionId = quizSessionId;
        QuestionId = questionId;
        QuestionOrder = questionOrder;
        Answered = false;
    }

    public void MarkAnswered()
    {
        Answered = true;
    }
}