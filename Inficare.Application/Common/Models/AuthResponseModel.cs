using Inficare.Domain.Enumerations;

namespace Inficare.Application.Common.Models
{
    public class AuthResponseModel
    {
        public string FullName { get; set; }
        public AuthTokenType TokenType { get; set; }
        public string Token { get; set; }
    }
}
