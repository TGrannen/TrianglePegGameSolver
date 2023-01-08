namespace TrianglePegGameSolver.Cloud.Infrastructure.Helpers;

public class AzureBudgetNotification
{
    public string? Name { get; init; }
    public double Threshold { get; init; } = 80;
    public ThresholdType ThresholdType { get; init; } = ThresholdType.Actual;
    public OperatorType Operator { get; init; } = OperatorType.GreaterThanOrEqualTo;

    public NotificationArgs Create(string[] notificationEmails)
    {
        return new NotificationArgs
        {
            ContactEmails = notificationEmails,
            Enabled = true,
            Locale = CultureCode.En_us,
            Operator = Operator,
            ThresholdType = ThresholdType,
            Threshold = Threshold,
        };
    }
}