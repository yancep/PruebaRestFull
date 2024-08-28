﻿using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersioningExtensions(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
