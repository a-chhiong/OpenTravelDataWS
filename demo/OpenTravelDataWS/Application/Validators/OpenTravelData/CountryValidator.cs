using System.Text.RegularExpressions;
using Application.Models.OpenTravelData.Country;

namespace Application.Validators.OpenTravelData;

public class CountryValidator: IValidator<CountryQuery>
{
    public ValidationResult Validate(CountryQuery input)
    {
        var code = input.Code;
        if (!Regex.IsMatch(code, @"^[A-Za-z]{2}$")
            && !Regex.IsMatch(code, @"^[A-Za-z]{3}$"))
        {
            return ValidationResult.Failure("The Code is invalid.");
        }

        return ValidationResult.Success();
    }
}