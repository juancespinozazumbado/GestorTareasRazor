﻿using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Web.Data;

public static class Dependencias
{
    public static IServiceCollection AgregarDbContext(this IServiceCollection services, IConfiguration configuration, bool useInMemory = false, bool useSqlite = false)
    {
        if (useInMemory)
        {


            services.AddDbContext<TareasContext>(options =>
                options.UseInMemoryDatabase("Tareasdb"));


        }
        if (useSqlite)
        {
            //var connectionString = configuration.GetConnectionString("SQLITECONNECTIONSTRING");
            var connectionString = Environment.GetEnvironmentVariable("SQLITECONNECTIONSTRING", EnvironmentVariableTarget.User);

            services.AddDbContext<TareasContext>(options =>
                options.UseSqlite(connectionString));
        }

        else
        {
            var connectionString = Environment.GetEnvironmentVariable("SQLSERVERCONNECTION");

            services.AddDbContext<TareasContext>(options =>
                options.UseSqlServer(connectionString));
        }


        return services;
    }
}
