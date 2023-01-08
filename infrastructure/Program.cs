using Pulumi.AzureNative.Authorization;
using TrianglePegGameSolver.Cloud.Infrastructure.Helpers;

return await Pulumi.Deployment.RunAsync(async () =>
{
    var clientConfig = await GetClientConfig.InvokeAsync();
    var config = new StackConfig { ClientConfig = clientConfig };

    var resourceGroup = new ResourceGroup($"{config.NameBase}-rg");
    config.ResourceGroupName = resourceGroup.Name;

    var site = new BlazorStaticWebApp { Config = config };

    var budget = new AzureBudget
    {
        Config = config,
        Amount = 3,
        Notifications = new[]
        {
            new AzureBudgetNotification
            {
                Threshold = 80,
                ThresholdType = ThresholdType.Actual,
                Name = "Actual_GreaterThanOrEqual_80_Percent"
            }
        }
    };

    var objects = new[]
    {
        site.Create(),
        budget.Create()
    };

    return objects.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
});