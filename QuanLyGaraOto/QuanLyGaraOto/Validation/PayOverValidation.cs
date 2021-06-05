using QuanLyGaraOto.Model;
using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyGaraOto.Validation
{
    public class PayOverValidation : ValidationRule
    {
        public string ErrorMessage { get; set; }
        public string ErrorMessageNull { get; set; }
        public string ErrorMessagePayOver { get; set; }
        public Window Window { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            RECEPTION reception = ((Window as PayWindow).DataContext as PayViewModel).Reception;
            if (value == null)
            {
                return new ValidationResult(true, null);
            }
            if (value.ToString() == "")
            {
                return new ValidationResult(false, this.ErrorMessageNull);
            }
            else if (regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
            else if ((DataProvider.Ins.DB.GARA_INFO.FirstOrDefault().IsOverPay == false) && ((int)value >  reception.Debt))
            {
                return new ValidationResult(false, this.ErrorMessagePayOver);
            }
            return new ValidationResult(true, null);
        }
    }
}
