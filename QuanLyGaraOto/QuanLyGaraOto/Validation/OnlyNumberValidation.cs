using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyGaraOto.Validation
{
    class OnlyNumberValidation : ValidationRule
    {
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(true, null);
            }
            if (value.ToString() == "")
            {
                return new ValidationResult(true, null);
            }
            else
            {
                Regex regex = new Regex(@"^[0-9]+$");
                return new ValidationResult(regex.IsMatch(value.ToString()), this.ErrorMessage);
            }
        }
    }
}