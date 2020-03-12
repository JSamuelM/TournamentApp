using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TournamentApp.Models
{
    class Validators
    {
        public static bool isNumber(string value)
        {
            return Regex.Match(value, RegularExpression.isNumber).Success;
        }
    }
}
