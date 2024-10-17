using MediatR;
using Application.Queries;
using Application.Dto;
using Domain.Repositories;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(usr => new UserDto
        {
            Email = usr.Email,
            Name = usr.Name,
            CreatedAt = usr.CreatedAt,
        }).ToList();
    }
}