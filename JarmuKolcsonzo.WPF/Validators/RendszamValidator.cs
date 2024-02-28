using System.Globalization;
using System.Windows.Controls;

namespace JarmuKolcsonzo.WPF.Validators
{
    public class RendszamValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? valueText = value.ToString();
            if (!string.IsNullOrWhiteSpace(valueText))
            {
                if (valueText.Length < 6 || valueText.Length > 8)
                {
                    return new ValidationResult(false, "A rendszám 7 vagy 8 karakterből állhat.");
                }
                if (!valueText.Contains("-"))
                {
                    return new ValidationResult(false, "A rendszám kötőjelet kell tartalmaznia.");
                }
                // Ha minden szabálynak megfelelt
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Kötelező mező.");
        }
    }
}