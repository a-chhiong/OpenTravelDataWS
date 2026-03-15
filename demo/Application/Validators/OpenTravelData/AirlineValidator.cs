using System.Text.RegularExpressions;
using Application.Models.OpenTravelData.Airline;

namespace Application.Validators.OpenTravelData;

public class AirlineValidator: IValidator<AirlineQuery>
{
    public ValidationResult Validate(AirlineQuery input)
    {
        var code = input.Code;
        if (!Regex.IsMatch(code, @"^[A-Za-z0-9]{2}$")
            && !Regex.IsMatch(code, @"^[A-Za-z0-9]{3}$"))
        {
            return ValidationResult.Failure("The Code is invalid.");
        }

        return ValidationResult.Success();
    }
}