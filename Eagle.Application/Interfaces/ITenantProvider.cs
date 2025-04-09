using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Application.Interfaces
{
    public interface ITenantProvider
    {
        string? GetTenant();
        void SetTenant(string tenantId);
    }
}
