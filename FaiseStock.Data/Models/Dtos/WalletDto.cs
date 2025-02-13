using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models.Dtos
{
    public class WalletDto
    {
        public string UserId { get; set; } = null!;

        public double Balance { get; set; }

    }
}
