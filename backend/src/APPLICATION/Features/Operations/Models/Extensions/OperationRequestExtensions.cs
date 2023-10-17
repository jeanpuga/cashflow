using System.Security.Claims;

namespace APPLICATION.Features.Operations.Models.Extensions
{
    public static class FilterRequestExtensions
    {
        public static FilterOperationRequest Mapper(this FilterRequest request, ClaimsPrincipal user) => new(request.Type, long.Parse(user.FindFirst(ClaimTypes.UserData).Value));
    }
}