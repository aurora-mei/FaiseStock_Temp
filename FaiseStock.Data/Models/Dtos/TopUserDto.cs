using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models.Dtos
{
    public class TopUserDto
    {
        public string userId { get; set; } = null!;

        public DateTime  createAt { get; set; }

        public int rank { get; set; }

        public double increasedAmount { get; set; }

        public double roic { get; set; }
        public string contestName { get; set; } = null!;

    }
}
