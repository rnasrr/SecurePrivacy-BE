namespace Application.Queries;

using MediatR;
using System.Collections.Generic;
using Application.Dto;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
}
