using LibrarySystem.Application.Repositories;
using LibrarySystem.Application.Services;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Infraestructure.Data;
using LibrarySystem.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibrarySystem.WinForms.Configuration
{
	public static class ServiceConfiguration
	{
		public static ServiceProvider Configure()
		{
			var services = new ServiceCollection();

			// Regista factory de conexões
			services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

			// Regista repositórios
			services.AddScoped<IUserRepository, UserRepository>();

			// Regista serviços
			services.AddScoped<IUserService, UserService>();

			return services.BuildServiceProvider();
		}
	}
}
