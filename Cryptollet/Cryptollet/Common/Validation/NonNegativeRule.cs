using System;
namespace Cryptollet.Common.Validation
{
    public class NonNegativeRule : IValidationRule<decimal>
    {
        public string ValidationMessage { get; set; }

        public bool Check(decimal value) => value > 0;
    }
}