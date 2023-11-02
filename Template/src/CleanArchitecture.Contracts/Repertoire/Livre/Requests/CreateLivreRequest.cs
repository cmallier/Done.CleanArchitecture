using FluentValidation;

namespace CleanArchitecture.Contracts.Repertoire.Livre.Requests;

// Request
public class CreateLivreRequest
{
    public string Titre { get; init; } = default!;
}


// Validation
public class CreateLivreRequestValidator : AbstractValidator<CreateLivreRequest>
{
    public CreateLivreRequestValidator()
    {
        RuleFor( x => x.Titre ).NotEmpty();
    }
}