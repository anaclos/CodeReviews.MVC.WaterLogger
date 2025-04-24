using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText =
                    $@"DELETE FROM drinking_water
                        WHERE Id = {id}";

                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToPage("./Index");
        }


        public DrinkingWaterModel GetById(int id)
        {
            var drinkingWater = new DrinkingWaterModel(); 
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText =
                    $@"SELECT * FROM drinking_water WHERE Id = {id}";
                SqliteDataReader reader = tableCommand.ExecuteReader();
                while (reader.Read())
                {
                    drinkingWater.Id = reader.GetInt32(0);
                    drinkingWater.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    drinkingWater.Unit = reader.GetString(2);
                    drinkingWater.Quantity = reader.GetInt32(3);
                }
                connection.Close();
            }
            return drinkingWater;
        }
    }
}
