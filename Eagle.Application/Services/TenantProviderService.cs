using Eagle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Application.Services
{
    public class TenantProviderService : ITenantProvider
    {
        private string? _tenantId;

        public string? TenantId
        {
            get => _tenantId;
            set => _tenantId = value; 
        }

        public string? GetTenant() => _tenantId;

        public void SetTenant(string tenantId)
        {
            _tenantId = tenantId;
        }
    }

}
