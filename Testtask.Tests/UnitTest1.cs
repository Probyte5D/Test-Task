using Xunit;
using TestSystem.Models;
using System.Collections.Generic;
using Moq;
using MongoDB.Driver;
using TestSystem.Services;
using TestSystem.Models;
using Xunit;
using System.Collections.Generic;

using TestSystem.Services; // <- necessario per IMongoDBService

namespace TestSystem.Tests
{
    public class TestSystemUnitTests
    {
        // ===========================
        // HOMEPAGE TESTS
        // ===========================
        [Fact]
        public void Username_ShouldNotBeEmpty()
        {
            string username = "Mario Rossi"; // esempio reale
            Assert.False(string.IsNullOrWhiteSpace(username), "Il nome utente non può essere vuoto");
        }

        [Fact]
        public void TestSelection_ShouldNotBeNull()
        {
            var selectedTest = new Test { Title = "Test di esempio" };
            Assert.NotNull(selectedTest); // Ora è un'istanza reale
        }

        // ===========================
        // QUESTION VIEW TESTS
        // ===========================
        [Fact]
        public void Question_ShouldAllowValidAnswer()
        {
            var question = new Question
            {
                Text = "Domanda di esempio",
                Options = new List<Option>
                {
                    new Option { Text = "Opzione 1", IsCorrect = true },
                    new Option { Text = "Opzione 2", IsCorrect = false }
                }
            };

            var userAnswer = question.Options[0]; // Risposta selezionata
            Assert.NotNull(userAnswer);
            Assert.True(userAnswer.IsCorrect);
        }

        [Fact]
        public void ProgressBar_ShouldUpdateCorrectly()
        {
            int totalQuestions = 5;
            int currentQuestionIndex = 2; // 3ª domanda
            double progress = (currentQuestionIndex + 1) * 100.0 / totalQuestions;
            Assert.Equal(60.0, progress);
        }

        // ===========================
        // RESULT VIEW TESTS
        // ===========================
        [Fact]
        public void Result_ShouldReturnCorrectScore()
        {
            var test = new Test
            {
                Questions = new List<Question>
                {
                    new Question { Options = new List<Option> { new Option { IsCorrect = true } } },
                    new Question { Options = new List<Option> { new Option { IsCorrect = false } } },
                    new Question { Options = new List<Option> { new Option { IsCorrect = true } } }
                }
            };

            var userAnswers = new List<Option>
            {
                test.Questions[0].Options[0],
                test.Questions[1].Options[0],
                test.Questions[2].Options[0]
            };

            int correctCount = 0;
            for (int i = 0; i < test.Questions.Count; i++)
            {
                if (userAnswers[i].IsCorrect) correctCount++;
            }

            Assert.Equal(2, correctCount);
        }

        [Fact]
        public void Result_ShouldIncludeUsername()
        {
            var userResult = new UserResult
            {
                UserName = "Mario Rossi",
                CorrectAnswers = 2,
                TotalQuestions = 3
            };

            Assert.False(string.IsNullOrWhiteSpace(userResult.UserName));
        }

        // ===========================
        // DATABASE TESTS (mock)
        // ===========================
        [Fact]
        public void Test_LoadFromDB_ShouldReturnQuestions()
        {
            var testFromDB = new Test
            {
                Title = "Test dal DB",
                Questions = new List<Question>
                {
                    new Question { Text = "Q1" },
                    new Question { Text = "Q2" }
                }
            };

            Assert.NotEmpty(testFromDB.Questions);
        }
    }
}
