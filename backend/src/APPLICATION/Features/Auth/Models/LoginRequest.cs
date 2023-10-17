using MediatR;
using System;
using System.ComponentModel;

namespace APPLICATION.Features.Auth.Models
{
    [Serializable]
    public class LoginRequest : IRequest<AuthResponse>
    {
        public LoginRequest()
        {
        }

        [DefaultValue("jean.puga@cashflow.com")]
        public string User { get; set; }

        [DefaultValue("123456")]
        public string Password { get; set; }
    }
}