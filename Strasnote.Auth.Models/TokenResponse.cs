using System;
using System.Collections.Generic;
using System.Text;

namespace Strasnote.Auth.Models
{
    public record TokenResponse(string AccessToken, string RefreshToken);
}
