using MediatR;

namespace Application.Commands;

public record CreateUserCommand : IRequest
{
    public string Name { get; }
    public string Email { get; }
    public bool ConsentToDataCollection { get; }

    public CreateUserCommand(string name, string email, bool consentToDataCollection)
    {
        Name = name;
        Email = email;
        ConsentToDataCollection = consentToDataCollection;
    }
}
