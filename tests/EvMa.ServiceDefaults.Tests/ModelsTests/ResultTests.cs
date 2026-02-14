using EvMa.ServiceDefaults.Models;

namespace EvMa.ServiceDefaults.Tests.ModelsTests
{
    public class ResultTests
    {
        [Fact]
        public void Success_Should_SetIsSuccessToTrue()
        {
            // Act
            var result = Result.Success();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public void Failure_Should_SetIsSuccessToFalse_And_AssignError()
        {
            // Arrange
            var error = new Error("Test.Code", "Test.Description");

            // Act
            var result = Result.Failure(error);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(error, result.Error);
        }

        [Fact]
        public void SuccessTValue_Should_ContainValue()
        {
            // Arrange
            var value = "Success Data";

            // Act
            var result = Result.Success(value);

            // Assert
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void FailureTValue_AccessingValue_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var result = Result.Failure<string>(new Error("Error", "Fail"));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Constructor_InvalidState_ShouldThrowArgumentException(bool isSuccess)
        {
            // Act & Assert
            if (isSuccess)
            {
                Assert.Throws<ArgumentException>(() => Result.Failure(Error.None));
            }
        }
    }
}
