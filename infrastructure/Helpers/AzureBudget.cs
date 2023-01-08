namespace TrianglePegGameSolver.Cloud.Infrastructure.Helpers;

public class AzureBudget
{
    public StackConfig Config { get; init; } = null!;
    public double Amount { get; init; } = 10;
    public AzureBudgetNotification[] Notifications { get; init; } = Array.Empty<AzureBudgetNotification>();

    public Dictionary<string, object?> Create()
    {
        var now = DateTime.UtcNow;
        var emailCsv = Config.PulumiConfig.Get("notification-emails") ?? string.Empty;
        var notificationEmails = emailCsv.Split(",")?.ToArray() ?? Array.Empty<string>();

        var budget = new Budget($"{Config.NameBase}-budget", new()
        {
            Amount = Amount,
            BudgetName = $"{Config.NameBase}-budget",
            Category = "Cost",
            Notifications = Notifications.ToDictionary(x => x.Name!, x => x.Create(notificationEmails)),
            Scope = Config.ResourceGroupName!.Apply(x => $"/subscriptions/{Config.ClientConfig.SubscriptionId}/resourceGroups/{x}"),
            TimeGrain = "Monthly",
            TimePeriod = new BudgetTimePeriodArgs
            {
                StartDate = new DateOnly(now.Year, now.Month, 1).ToShortDateString()
            },
        });
        return new Dictionary<string, object?>();
    }
}