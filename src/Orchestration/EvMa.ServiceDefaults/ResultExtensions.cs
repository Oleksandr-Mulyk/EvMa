using EvMa.ServiceDefaults.Models;
using FluentValidation.Results;

namespace EvMa.ServiceDefaults
{
    public static class ResultExtensions
    {
        public static Result ToResult(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return Result.Success();

            var error = new Error(
                validationResult.Errors[0].ErrorCode,
                validationResult.Errors[0].ErrorMessage);

            return Result.Failure(error);
        }
    }
}