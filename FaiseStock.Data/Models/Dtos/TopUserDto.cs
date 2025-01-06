using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models.Dtos
{
    public class TopUserDto
    {
        public string UserId { get; set; } = null!;

        public DateTime CreateAt { get; set; }

        public int Rank { get; set; }

        public double IncreasedAmount { get; set; }

        public double Roic { get; set; }
        public string UserName { get; set; } = null!;

    }
}
