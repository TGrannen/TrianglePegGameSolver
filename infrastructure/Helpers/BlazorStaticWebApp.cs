namespace TrianglePegGameSolver.Cloud.Infrastructure.Helpers;

public class BlazorStaticWebApp
{
    public StackConfig Config { get; init; } = null!;

    public Dictionary<string, object?> Create()
    {
        var siteName = $"{Config.NameBase}-site";
        var staticSiteId = Config.ResourceGroupName.Apply(async rgName =>
        {
            try
            {
                var site = await GetStaticSite.InvokeAsync(new GetStaticSiteArgs
                {
                    ResourceGroupName = rgName,
                    Name = siteName,
                });
                return site.Id;
            }
            catch (Exception)
            {
                Console.WriteLine($"{siteName} was not found when searched for");
                return string.Empty;
            }
        });

        var staticSite = new StaticSite($"{Config.NameBase}-site", new StaticSiteArgs
        {
            Branch = "main",
            BuildProperties = new Pulumi.AzureNative.Web.Inputs.StaticSiteBuildPropertiesArgs
            {
                ApiLocation = Config.APILocation,
                AppLocation = "temp/client/wwwroot",
                OutputLocation = ""
            },
            Location = "eastus2",
            Name = siteName,
            RepositoryToken = Config.Token,
            RepositoryUrl = Config.RepoName,
            ResourceGroupName = Config.ResourceGroupName,
            Sku = new Pulumi.AzureNative.Web.Inputs.SkuDescriptionArgs
            {
                Name = "Free",
                Tier = "Free",
            }
        });

        staticSiteId.Apply(id =>
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var domain = new StaticSiteCustomDomain($"{Config.NameBase}-site-custom-domain", new()
            {
                DomainName = Config.Domain,
                Name = staticSite.Name,
                ResourceGroupName = Config.ResourceGroupName,
            }, new CustomResourceOptions { DeleteBeforeReplace = true });
            return domain;
        });

        return new Dictionary<string, object?>
        {
            { "static-site-default-hostname", staticSite.DefaultHostname },
            { "static-site-domains", staticSite.CustomDomains },
        };
    }
}