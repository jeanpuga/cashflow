namespace APPLICATION.Features.Operations.Models.Extensions
{
    public static class SubmitOperationExtensions
    {
        public static Operation Mapper(this SubmitOperationMessage message)
        {
            return new(
                message.AccountId,
                message.Describe,
                message.Value,
                message.Type
                );
        }
    }
}