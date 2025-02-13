namespace FaiseStock.Data.Models.ViewModels;

public class ContestVM
{
    public string ContestId { get; set; } = null!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string ContestName { get; set; }
}