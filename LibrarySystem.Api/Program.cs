using LibrarySystem.Application.Persistence;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Infraestructure.Data;
using LibrarySystem.Infraestructure.Repositories;
using LibrarySystem.Infraestructure.Persistence;

namespace LibrarySystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Register database connection factory
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddSingleton<IDbConnectionFactory>(
				new SqlServerConnectionFactory(connectionString!)
			);

			// Register repositories (for standalone use)
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			// Register Unit of Work (for transactional use)
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
