using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infra.Data.Repositories;
using Blog.Application.Interfaces;
using Blog.Application.Services;

namespace Blog.Infra.IoC
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.TryAddScoped<ICommentRepository, CommentRepository>();
            services.TryAddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}
