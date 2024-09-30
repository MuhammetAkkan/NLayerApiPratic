﻿using App.Repositories.Models.Products;
using App.Repositories.RepositoryPattern;
using App.Repositories.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>(); //GetSection ile bu alana ulaşıyor, Get ile de nesneyi alıyoruz.

            options.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName); //migration işlemleri için gerekli
            });

        });

        //scoped lar dbContext ile ilgili olduğu için burada tanımlanmalıdır.

        //genel scoped
        services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));

        //Product scoped
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
        // Add your repository extensions here

    }
}
