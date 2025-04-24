using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List <DrinkingWaterModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        private List<DrinkingWaterModel> GetAllRecords()
        {
            using(var connection = new SqliteConnection(_configuration.GetConnectionString("connectionString")))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText =
                    "SELECT * FROM drinking_water";

                var tableData = new List<DrinkingWaterModel>();

                SqliteDataReader reader = tableCommand.ExecuteReader();
                while (reader.Read())
                {
                    tableData.Add(new DrinkingWaterModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Unit = reader.GetString(2),
                        Quantity = reader.GetInt32(3)
                    });
                }
                connection.Close();
                return tableData;
            }
        }
    }
}
