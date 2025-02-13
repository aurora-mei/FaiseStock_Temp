namespace FaiseStock.Utilities.Converters;

public class ConvertCronExpression: IConvertCronExpression
{
    public ConvertCronExpression()
    {
        
    }
    public string convertToCronExpression(DateTime cronExpression)
    {
        string seconds = cronExpression.Millisecond.ToString();
        string minutes = cronExpression.Minute.ToString();
        string hours = cronExpression.Hour.ToString();
        string dayOfMonth = cronExpression.Day.ToString();
        string month = cronExpression.Month.ToString();
        string year = cronExpression.Year.ToString();
        return seconds + " " +  minutes + " " +hours+ " " + dayOfMonth + " " +month +" ? "+ year;
    }

    public DateTime convertFromCronExpression(string cronExpression)
    {
        string cronExpressionPart = cronExpression.Replace("? ", "");
        string[] cronExpressionParts = cronExpressionPart.Split(' ');
        string endTime = "2024-02-11 10:30:00";
        string seconds = cronExpressionParts[0];
        string minutes = cronExpressionParts[1];
        string hours = cronExpressionParts[2];
        string dayOfMonth = cronExpressionParts[3];
        string month = cronExpressionParts[4];
        string year = cronExpressionParts[5];
        string endDate = year + "-" + month + "-" + dayOfMonth;
        endTime = hours + ":" + minutes + ":" + seconds;
        DateTime finalTime = DateTime.Parse(endDate + " " + endTime);
        return finalTime;
    }
}