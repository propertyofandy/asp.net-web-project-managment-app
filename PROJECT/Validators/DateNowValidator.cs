using System.ComponentModel.DataAnnotations;

namespace PROJECT.Validators
{
    public class DateNowValidator : ValidationAttribute
    {

        public override bool IsValid(object? value) // will allow for date input within the last year. 
        {
            if (value == null) return false; // if null failed

            DateTime end = DateTime.Now; // get current date
            DateTime begin = DateTime.Now.AddYears(-1); // get 1 year from past
            DateTime check; 
            
            return DateTime.TryParse(value.ToString() , out check) // check if parsable then check in bounds
                    && check <= end
                    && check >= begin;
        }
    }
}
