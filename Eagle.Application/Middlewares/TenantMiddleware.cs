using Eagle.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITenantProvider tenantProvider)
        {
            var tenantId = context.Request.Headers["X-Tenant-ID"].FirstOrDefault();

            if (!string.IsNullOrEmpty(tenantId))
            {
                tenantProvider.SetTenant(tenantId); 
            }
            else
            {
                tenantProvider.SetTenant("default");
            }

            await _next(context);
        }
    }

}
