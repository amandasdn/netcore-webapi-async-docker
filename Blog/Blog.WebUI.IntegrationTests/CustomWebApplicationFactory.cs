using Blog.Application.Interfaces;
using Blog.Infra.Data.Messages;
using Blog.WebUI.IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blog.WebUI.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var queue = services.Single(s => s.ImplementationType == typeof(CommentMessageConsumer));
                services.Remove(queue);

                services.RemoveAll(typeof(ICommentService));
                services.TryAddScoped<ICommentService, CommentServiceMock>();
            });
        }
    }
}