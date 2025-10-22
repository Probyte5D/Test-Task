using System.Collections.Generic;

namespace TestSystem.Models
{
    public class Test
{
    public string? Id { get; set; }  // <- ora non è più richiesto
    public string Title { get; set; } = string.Empty;
    public List<Question> Questions { get; set; } = new List<Question>();
}

public class Question
{
    public string? Id { get; set; }  // <- ora non è più richiesto
    public string Text { get; set; } = string.Empty;
    public List<Option> Options { get; set; } = new List<Option>();
}

public class Option
{
    public string? Id { get; set; }  // <- ora non è più richiesto
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
}

public class UserResult
{
    public string? Id { get; set; }  // <- ora non è più richiesto
    public string UserName { get; set; } = string.Empty;
    public string TestId { get; set; } = string.Empty;
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
}
