using System.ComponentModel.DataAnnotations;

namespace PROJECT.Validators
{
    public class BalanceVlidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return (value == null)?
                    false:
                    (Double.Parse(value.ToString()) >= 0);
        }
    }
}
