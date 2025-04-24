using Microsoft.AspNetCore.Mvc.Rendering;

namespace WaterLogger.Models;

public class DrinkingWaterModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Unit {  get; set; }
    public int Quantity { get; set; }

    public static List<SelectListItem> GetUnits()
    {
        List<SelectListItem> list= new List<SelectListItem>
        {
            new SelectListItem { Text = "Big Bottle", Value = "Big Bottle" },
            new SelectListItem { Text = "Bottle", Value = "Bottle" },
            new SelectListItem { Text = "Glass", Value = "Glass" }
        };
        return list;
    }
}