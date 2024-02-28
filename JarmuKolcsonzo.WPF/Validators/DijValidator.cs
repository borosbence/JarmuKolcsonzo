using System.Globalization;
using System.Windows.Controls;

namespace JarmuKolcsonzo.WPF.Validators
{
    public class DijValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                decimal.TryParse(value.ToString(), out decimal number);
                if (number > 0)
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "A mező csak 0-nál nagyobb számot tartalmazhat.");
        }
    }
}
