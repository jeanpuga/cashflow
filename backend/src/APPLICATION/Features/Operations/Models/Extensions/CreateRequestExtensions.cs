using System.Security.Claims;

namespace APPLICATION.Features.Operations.Models.Extensions
{
    public static class CreateRequestExtensions
    {
        public static CreateOperationRequest Mapper(this CreateRequest request, ClaimsPrincipal user) =>
            new(request.Value, request.Describe, request.Type, long.Parse(user.FindFirst(ClaimTypes.UserData).Value));
    }
}