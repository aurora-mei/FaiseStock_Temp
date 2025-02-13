namespace FaiseStock.Data.Models.ViewModels;

public class ContestParticipantVM
{
    public string ContestId { get; set; }
    public string UserId { get; set; }
    public string ContestName { get; set; }
    public string UserName { get; set; }
    public double InitialBalance { get; set; }
    public double FinalBalance { get; set; }
}