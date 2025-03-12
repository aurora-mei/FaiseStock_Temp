namespace FaiseStock.Data.Models.ViewModels;

public class ContestParticipantVM
{
    public string contestId { get; set; }
    public string userId { get; set; }
    public double initialBalance { get; set; }
    public double finalBalance { get; set; }
}