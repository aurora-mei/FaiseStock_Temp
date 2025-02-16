using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models.Dtos
{
    public class ContestDto
    {
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string contestName { get; set; }
    }
}
