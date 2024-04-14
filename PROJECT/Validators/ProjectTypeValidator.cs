using System.ComponentModel.DataAnnotations;

namespace PROJECT.Validators
{
    public class ProjectTypeValidator : ValidationAttribute
    {
        public override bool IsValid(object? value) // checks if it the right enum 
        {
            if(value == null) return false; // if null false

            var type = value.GetType(); // get values type

            return type.IsEnum && Enum.IsDefined(type, value); // check if type is Enum and that it is defined
        }
    }
}
