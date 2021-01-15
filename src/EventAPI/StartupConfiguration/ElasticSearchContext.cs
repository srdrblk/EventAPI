using Nest;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;

namespace EventAPI.StartupConfiguration
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticSearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultMappingFor<Event>(e=>e.PropertyName(a=>a.Id, "id").PropertyName(a=>a.Title,"title").IndexName(defaultIndex));

            var client = new ElasticClient(settings);


            services.AddSingleton<IElasticClient>(client);
        }
    }
}
