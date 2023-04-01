using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            
            var cs = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePost).GetTypeInfo().Assembly));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
        }

        public static void RegisterEndPointDefinitions(this WebApplication app)
        {
            var endPointDefinitions = typeof(Program).Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach(var endpointDef in endPointDefinitions)
            {
                endpointDef.RegisterEndpoints(app);
            }
        }
    }
}
