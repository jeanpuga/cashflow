using APPLICATION.Features.Operations.Models.Enums;

namespace APPLICATION.Features.Operations.Models
{
    public class CreateRequest
    {
        public string Describe { get; set; }
        public decimal Value { get; set; }
        public TypeTransaction Type { get; set; }
    }
}