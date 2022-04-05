using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Models
{
    public static class Calculator
    {
        public static int Adder(params int[] numbers)
        {
            return numbers.Sum();
        }
    }
}
