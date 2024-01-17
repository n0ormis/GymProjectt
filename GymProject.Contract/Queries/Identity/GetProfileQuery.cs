using GymProject.Contract.Dtos.Auth;
using MediatR;

namespace GymProject.Contract.Queries.Identity;

public sealed record GetProfileQuery : IRequest<ProfileDto>;
