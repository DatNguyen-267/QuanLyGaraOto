﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyGaraOto.Validation
{
    public class NullDataValidation : ValidationRule
    {
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(true, null);
            if (value.ToString() == "") return new ValidationResult(false, ErrorMessage);
            return new ValidationResult(true, null);
        }
    }
}
