namespace CleanArchitecture.Application.Abstractions.Behaviors;

public sealed record class ValidationError(string PropertyName, string ErrorMessage);