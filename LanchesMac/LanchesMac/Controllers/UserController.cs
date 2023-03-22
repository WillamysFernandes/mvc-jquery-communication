using Dapper;
using Dapper.Contrib.Extensions;
using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace LanchesMac.Controllers
{
	public class UserController : Controller
	{
		private const string connectionString = "Data Source=DESKTOP-8U0OIEV\\SQLEXPRESS1;Initial Catalog=CADASTRO;Integrated Security=True;TrustServerCertificate=True";

		public IActionResult New()
		{
			var model = new EditViewModel();

			return View("Edit", model);
		}
		public IActionResult List()
		{
			var model = new UserViewModel();
			model.Users = GetAll();
			return View(model);
		}

		[HttpPost]
		public ActionResult Save([FromBody] User user)
		{
			if (user.Id == 0)
			{
				SqlExcute("INSERT INTO [User] VALUES (@Nome, @Idade)", new { user.Nome, user.Idade });
			}
			else
			{
				SqlExcute("UPDATE [User] SET Nome=@Nome, Idade=@Idade Where Id=@Id", new { user.Id, user.Nome, user.Idade });
			}

			return Ok(new ContentResultModel
			{
				Status = true
			});
		}

		public List<User> GetAll()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "SELECT* FROM [User]";

				return connection.Query<User>(query).ToList();
			}
		}

		public IActionResult Edit(int Id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "SELECT * FROM [User] where Id =@Id ";
				var user = connection.Query<User>(query, new
				{
					Id = Id
				}).FirstOrDefault();

				var model = new EditViewModel
				{
					User = user
				};
				return View(model);
			}
		}
		[HttpDelete]
		public IActionResult Delete(int Id)
		{
			SqlExcute("DELETE FROM [User] where Id =@Id", new { Id = Id });

			return Ok(new ContentResultModel
			{
				Status = true
			});
		}
		private void SqlExcute(string query, object parameters)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Execute(query, parameters);
			}
		}
		private void SqlQuery(string query, object parameters)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Query(query, parameters);
			}
		}
	}
}
