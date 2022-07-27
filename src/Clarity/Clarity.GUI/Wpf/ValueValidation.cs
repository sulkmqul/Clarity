using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Clarity.GUI.Wpf
{
    /// <summary>
    /// Int検証
    /// </summary>
    public class IntValidation : ValidationAttribute
    {
        public IntValidation(string ermes) : base(ermes)
        {

        }

        public override bool IsValid(object? value)
        {
            int m;
            return int.TryParse(value?.ToString(), out m);
        }
    }


    /// <summary>
    /// Doubleの検証
    /// </summary>
    public class DoubleValidation : ValidationAttribute
    {
        public DoubleValidation(string ermes) : base(ermes)
        {

        }

        public override bool IsValid(object? value)
        {
            double m;
            return double.TryParse(value?.ToString(), out m);
        }
    }
}
