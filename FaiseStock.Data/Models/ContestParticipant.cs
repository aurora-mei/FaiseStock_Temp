namespace FaiseStock.Data.Models;

public class ContestParticipant
{
    public string contestId { get; set; }
    public string userId { get; set; }
    public double initialBalance { get; set; }
    public double? finalBalance { get; set; }

    public virtual Contest contest { get; set; }
    public virtual User user { get; set; }
}
