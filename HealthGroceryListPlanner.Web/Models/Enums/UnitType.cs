using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Web.Models.Enums
{
    public enum UnitType
    {
        [Display(Name = "pcs")]
        Pieces = 0,

        [Display(Name = "kg")]
        Kilogram = 1,

        [Display(Name = "g")]
        Gram = 2,

        [Display(Name = "L")]
        Liter = 3,

        [Display(Name = "ml")]
        Milliliter = 4
    }
}