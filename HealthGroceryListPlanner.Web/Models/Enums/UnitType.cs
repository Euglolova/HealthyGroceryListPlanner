using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Web.Models.Enums
{
    public enum UnitType
    {
        [Display(Name = "pcs")]
        Pieces,

        [Display(Name = "kg")]
        Kilogram,

        [Display(Name = "g")]
        Gram,

        [Display(Name = "L")]
        Liter,

        [Display(Name = "ml")]
        Milliliter
    }
}