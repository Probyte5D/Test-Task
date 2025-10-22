using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TestSystem.Models
{
    public class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Title { get; set; } = string.Empty;

        public List<Question> Questions { get; set; } = new List<Question>();
    }

    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Text { get; set; } = string.Empty;

        public List<Option> Options { get; set; } = new List<Option>();
    }

    public class Option
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;
    }

    public class UserResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string UserName { get; set; } = string.Empty;

        public string TestId { get; set; } = string.Empty;

        public int CorrectAnswers { get; set; }

        public int TotalQuestions { get; set; }
    }
}
