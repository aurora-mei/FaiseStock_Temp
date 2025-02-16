namespace FaiseStock.Data.Models.ViewModels;

public class ContestVM
{
    public string contestId { get; set; } = null!;
    public DateTime startDateTime { get; set; }
    public DateTime endDateTime { get; set; }
    public string contestName { get; set; }
}