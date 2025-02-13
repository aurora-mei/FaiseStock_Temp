namespace FaiseStock.Data.Models;

public class ContestParticipant
{
    public string ContestId { get; set; }
    public string UserId { get; set; }
    public double InitialBalance { get; set; }
    public double? FinalBalance { get; set; }

    public virtual Contest Contest { get; set; }
    public virtual User User { get; set; }
}
