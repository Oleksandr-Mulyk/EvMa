using EvMa.ServiceDefaults.Models;
using FluentValidation.Results;

namespace EvMa.ServiceDefaults.Tests
{
    public class ResultExtensions
    {
        [Fact]
        public void ToResult_ShouldReturnSuccess_WhenValidationIsValid()
        {
            // Arrange
            var validationResult = new ValidationResult();

            // Act
            var result = validationResult.ToResult();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Error.None, result.Error);
        }

        [Fact]
        public void ToResult_ShouldReturnFailure_WhenValidationHasErrors()
        {
            // Arrange
            string errorCode = "User.Name.Empty";
            string errorMessage = "Username is required";

            var failures = new List<ValidationFailure> { new("UserName", errorMessage) { ErrorCode = errorCode } };
            var validationResult = new ValidationResult(failures);

            // Act
            var result = validationResult.ToResult();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(errorCode, result.Error.Code);
            Assert.Equal(errorMessage, result.Error.Description);
        }

        [Fact]
        public void ToResult_ShouldTakeFirstError_WhenMultipleErrorsExist()
        {
            // Arrange
            var failures = new List<ValidationFailure>
            {
                new("Email", "Invalid format") { ErrorCode = "Email.Invalid" },
                new("Password", "Too short") { ErrorCode = "Password.Short" }
            };
            var validationResult = new ValidationResult(failures);

            // Act
            var result = validationResult.ToResult();

            // Assert
            Assert.Equal("Email.Invalid", result.Error.Code);
        }
    }
}
