using FluentValidation.Results;

namespace Ordering.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException() 
        : base("One or more validation failures have occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }
 
    public ValidationException(IEnumerable<ValidationFailure> failtures) : this()
    {
        Errors = failtures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failuresGroup => failuresGroup.ToArray());
    }
}