using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }
        public List<SelectListItem> UnitOptions {  get; set; }

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            UnitOptions = DrinkingWaterModel.GetUnits();
            return Page();
        }        

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                UnitOptions = DrinkingWaterModel.GetUnits();
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText =
                    $@"INSERT INTO drinking_water(date,unit,quantity)
                        VALUES('{DrinkingWater.Date}','{DrinkingWater.Unit}',{DrinkingWater.Quantity})";

                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToPage("./Index");
        }
    }
}