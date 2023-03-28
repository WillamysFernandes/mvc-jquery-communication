using Dapper;
using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LanchesMac.Controllers
{
    public class AuthenticateController : Controller
    {
		private const string connectionString = "Data Source=DESKTOP-8U0OIEV\\SQLEXPRESS1;Initial Catalog=CADASTRO;Integrated Security=True;TrustServerCertificate=True";

		public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public IActionResult LoginCompare([FromBody] LoginViewModel loginViewModel)
		{
			var user = SqlQueryUser("SELECT * FROM[User] Where Login = @login and Password = @password", new { login = loginViewModel.Login, password = loginViewModel.Password });

			if (user != null)
			{
				return Ok(new LoginResultModel
				{
					Status = true
				});

			}
			return View();
		}

		private User SqlQueryUser(string query, object parameters)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var user = connection.Query<User>(query, parameters);

				return user.FirstOrDefault(); ;
			}
		}
	}
}
