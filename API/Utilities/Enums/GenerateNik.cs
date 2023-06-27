using System.ComponentModel.DataAnnotations;

namespace API.Utilities.Enums
{
    public class GenerateNik : ValidationAttribute
    {
        /*public override string IsValid(object? value)
        {
            var nik = (string)value;
            if (nik is null)
            {
                return "11111";
            }
            else
            {
                string lastNik = GetLastEmployeeNik();
                int nextNik = int.Parse(lastNik) + 1;
                return nextNik.ToString();
            }
        }*/
    }
}
