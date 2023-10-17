namespace APPLICATION.Features.Operations.Models.Extensions
{
    public static class OperationExtensions
    {
        internal static object Mapper(this CreateOperationRequest request)
        {
            return new
            {
                request.AccountId,
                request.Describe,
                request.Value,
                request.Type
            };
        }
    }
}