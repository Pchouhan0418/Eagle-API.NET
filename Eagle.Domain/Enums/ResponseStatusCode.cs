using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Enums
{
    public enum ResponseStatusCode
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        NotFound = 404,
        Unauthorized = 401,
        InternalServerError = 500,
        Conflict = 409,
        PaymentRequired = 402,
        Forbidden = 403,
        InvalidEmailOrPassword = 1001,
        Failure = 1,

    }
}
