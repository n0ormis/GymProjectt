using GymProject.Contract.Dtos.Auth;
using GymProject.Contract.Queries.Identity;
using GymProject.Core.Auth;
using MediatR;

namespace GymProject.Core.Queries.Identity;

internal sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileDto>
{
    private readonly IIdentityService _identityService;

    public GetProfileQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        => _identityService.GetCurrentUser();
    
}