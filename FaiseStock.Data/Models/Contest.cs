using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models
{
    public class Contest
    {
        public string contestId { get; set; } = null!;
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string contestName { get; set; }
        
        public virtual ICollection<TopUser> topUsers { get; set; } = new List<TopUser>();
        public virtual ICollection<ContestParticipant> contestParticipants { get; set; } = new List<ContestParticipant>();

    }
}
