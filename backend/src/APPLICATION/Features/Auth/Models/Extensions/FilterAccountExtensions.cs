using System;
using System.Text;

namespace APPLICATION.Features.Auth.Models.Extensions
{
    public static class FilterAccountExtensions
    {
        public static FilterAccount Mapper(this LoginRequest request)
        {
            var bytes = Encoding.ASCII.GetBytes(request.Password);
            var password = Convert.ToBase64String(bytes);

            return new FilterAccount(request.User, password);
        }
    }
}