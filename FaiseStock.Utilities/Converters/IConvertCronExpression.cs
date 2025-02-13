using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Utilities.Converters
{
    public interface IConvertCronExpression
    {
        public string convertToCronExpression(DateTime cronExpression);
        public DateTime convertFromCronExpression(string cronExpression);
    }
}
