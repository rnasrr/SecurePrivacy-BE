using Domain.Repositories;
using MediatR;
using Application.Commands;
using Domain.Entities;


namespace Application.Handlers;


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
