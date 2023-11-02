using CleanArchitecture.Application.Abstractions;

namespace CleanArchitecture.Infrastructure.Email;

public class EmailService : IEmailService
{
    public Task SendEmailAsync( string to, string subject, string body )
    {
        throw new NotImplementedException();
    }
}
