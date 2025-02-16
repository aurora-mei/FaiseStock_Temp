using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models.Dtos
{
    public class WalletDto
    {
        public string userId { get; set; } = null!;

        public double balance { get; set; }

    }
}
