using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WeatherStationProject.Dashboard.Data.Validations
{
    public class GroupingRangeAttribute : ValidationAttribute
    {
        private static readonly string[] AllowedValues = {GroupingValues.Hours.ToString(),
            GroupingValues.Days.ToString(),
            GroupingValues.Months.ToString()};

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return AllowedValues.Contains(value) ? ValidationResult.Success : new ValidationResult("Please, enter a valid grouping");
        }
    }
}