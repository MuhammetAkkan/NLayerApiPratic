using App.Service.ExceptionHandler;
using App.Service.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using App.Service.Categories;

namespace App.Service.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();


       
        //autoMapper added
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); //AutoMapper ekledik.

        //ExceptionHandler added
        services.AddExceptionHandler<CriticalExceptionHandler>(); //ExceptionHandler ekledik.
        services.AddExceptionHandler<GlobalExceptionHandler>();

        //FluentValidation added
        services.AddFluentValidationAutoValidation(); 
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        



        return services;
        // Add your repository extensions here

    }
}
