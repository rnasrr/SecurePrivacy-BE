using Domain.Repositories;
using MediatR;
using Domain.Entities;
using FluentValidation;


namespace Application.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("A valid email is required.");
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Create a new User entity
        var user = new User(request.Name, request.Email, request.ConsentToDataCollection);

        // Store the user using the repository
        await _userRepository.AddAsync(user);
    }
}
