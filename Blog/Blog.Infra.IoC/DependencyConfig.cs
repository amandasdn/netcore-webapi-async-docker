using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infra.Data.Repositories;
using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Domain.Interfaces.Integration;
using Blog.Infra.Data.Messages;

namespace Blog.Infra.IoC
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.TryAddScoped<ICommentRepository, CommentRepository>();
            services.TryAddScoped<ICommentService, CommentService>();

            services.AddSingleton<IMessageSender, MessageSender>();

            return services;
        }
    }
}
