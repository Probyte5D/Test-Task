using Xunit;
using TestSystem.Models;

namespace TestSystem.Tests
{
    public class TestTests
    {
        [Fact]
        public void Test_Creation_ShouldHaveEmptyQuestions()
        {
            var test = new Test();
            Assert.NotNull(test.Questions);
            Assert.Empty(test.Questions);
        }

        [Fact]
        public void Question_ShouldHaveOptions()
        {
            var question = new Question { Text = "Domanda di esempio" };
            question.Options.Add(new Option { Text = "Risposta 1", IsCorrect = true });

            Assert.Single(question.Options);
            Assert.True(question.Options[0].IsCorrect);
        }
    }
}
