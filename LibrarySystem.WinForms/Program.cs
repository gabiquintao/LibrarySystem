using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.WinForms.Configuration;
using LibrarySystem.WinForms.Forms.Users;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.WinForms
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();

			var serviceProvider = ServiceConfiguration.Configure();

			var userService = serviceProvider.GetRequiredService<IUserService>();

			System.Windows.Forms.Application.Run(new UsersForm(userService));
		}
	}
}
