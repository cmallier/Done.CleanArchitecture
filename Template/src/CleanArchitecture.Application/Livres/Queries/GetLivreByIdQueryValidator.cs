using FluentValidation;

namespace CleanArchitecture.Application.Livres.Queries;

public class GetLivreByIdQueryValidator : AbstractValidator<GetLivreByIdQuery>
{
    public GetLivreByIdQueryValidator()
    {
        RuleFor( c => c.Id )
            .LessThanOrEqualTo( 5 )
            .NotEmpty();
    }
}