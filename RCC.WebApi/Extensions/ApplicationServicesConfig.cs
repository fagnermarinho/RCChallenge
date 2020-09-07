using Microsoft.Extensions.DependencyInjection;
using RCC.Core.Repositories;
using RCC.Core.Services;
using RCC.Core.Services.Imp;
using RCC.Infrastructure.Repositories.SqlLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RCC.WebApi.Extensions
{
    public static class ApplicationServicesConfig
    {
        public static IMvcBuilder MapApplicationServices(this IMvcBuilder builder)
        {
            builder.Services.AddTransient<ILikeRepository, LikeRepository>();
            builder.Services.AddTransient<IArticleLikeRepository, ArticleRepository>();
            builder.Services.AddTransient<ILikeService, LikeService>();
            builder.Services.AddTransient<IArticlesLikeService, ArticleService>();

            return builder;
        }
    }
}
