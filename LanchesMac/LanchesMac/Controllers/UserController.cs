using Dapper;
using Dapper.Contrib.Extensions;
using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


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

			var newUser = new User()
			{
				Id = user.Id,
				Nome = user.Nome,
				Idade = user.Idade
			};
			if (user.Id == 0)
			{
				using (var connection = new SqlConnection(connectionString))
				{

					var query = "INSERT INTO [User] VALUES (@Nome, @Idade)";
					connection.Execute(query, new { newUser.Nome, newUser.Idade });

				}
			}
			else
			{
				using (var connection = new SqlConnection(connectionString))
				{

					var query = "UPDATE [User] SET Nome=@Nome, Idade=@Idade Where Id=@Id";
					connection.Execute(query, new { newUser.Id, newUser.Nome, newUser.Idade });

				}
			}

			var result = new ContentResultModel
			{
				Status = true,
				Message = "Usuário salvo com sucesso!"

			};
			return Ok(result);
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
		public IActionResult Delete(int Id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var query = "DELETE FROM [User] where Id =@Id ";
				connection.Execute(query, new
				{
					Id = Id
				});
			}
			var model = new UserViewModel();
			model.Users = GetAll();
			return View("List", model);
		}
	}
}
