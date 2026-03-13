using System.Text.RegularExpressions;
using Application.Models.OpenTravelData.Airport;

namespace Application.Validators.OpenTravelData;

public class AirportValidator: IValidator<AirportQuery>
{
    public ValidationResult Validate(AirportQuery input)
    {
        var code = input.Code;
        if (!Regex.IsMatch(code, @"^[A-Za-z]{3}$"))
        {
            return ValidationResult.Failure("The Code is invalid.");
        }

        return ValidationResult.Success();
    }
}