using Dapper;
using Dapper.Contrib.Extensions;
using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;
using System.Text;

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
		public IActionResult List(User user)
		{

			var model = new UserViewModel();

			model.Users = GetAll();
			return View(model);
		}

		[HttpPost]
		public ActionResult Save([FromBody] User user)
		{
			string passWord = CriarHash(user.Password);
			var DataNascimento = Convert.ToDateTime(user.DataNascimento).Date;
			var DataAtualizacao = DateTime.Now;
			var DataCriacao = DateTime.Now;
			if (user.Id == 0)
			{
				SqlExcute("INSERT INTO [User] VALUES (@Nome, @Idade, @Sexo, @DataCriacao, @DataAtualizacao, @Login, @Password, @DataNascimento, @GitHub)",
					new { user.Nome, user.Idade, user.Sexo, DataCriacao, DataAtualizacao, user.Login, passWord, DataNascimento, user.GitHub });
			}
			else
			{
				SqlExcute("UPDATE [User] SET Nome=@Nome, Idade=@Idade, Sexo=@Sexo, DataCriacao=@DataCriacao, DataAtualizacao=@DataAtualizacao, Login=@Login, DataNascimento=@DataNascimento, GitHub=@GitHub Where Id=@Id",
					new {user.Nome, user.Idade, user.Sexo, DataCriacao, DataAtualizacao, user.Login, DataNascimento, user.GitHub, user.Id});
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
		private string CriarHash(string pass)
		{
			var md5 = MD5.Create();
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(pass);
			byte[] hash = md5.ComputeHash(bytes);

			StringBuilder stringPass = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				stringPass.Append(hash[i].ToString("X2"));
			}
			return stringPass.ToString();
		}
	}
}
