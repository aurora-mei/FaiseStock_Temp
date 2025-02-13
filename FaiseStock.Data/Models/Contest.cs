using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaiseStock.Data.Models
{
    public class Contest
    {
        public string ContestId { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ContestName { get; set; }
        
        public virtual ICollection<TopUser> TopUsers { get; set; } = new List<TopUser>();
        public virtual ICollection<ContestParticipant> ContestParticipants { get; set; } = new List<ContestParticipant>();

    }
}
