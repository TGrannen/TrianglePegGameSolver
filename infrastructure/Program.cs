using Pulumi.Cloudflare;
using Pulumi.Cloudflare.Inputs;
using Config = Pulumi.Config;
using Deployment = Pulumi.Deployment;

return await Deployment.RunAsync(async () =>
{
    var fconfig = new Config("triangle-peg-game");
    var accountId = fconfig.Require("accountId");
    var domain = fconfig.Require("domain");
    const string subdomain = "trianglepeggame";
    const string pagesProjectName = "triangle-peg-game";

    var zone = GetZone.Invoke(new GetZoneInvokeArgs { Name = domain });

    var pagesProject = new PagesProject("triangle-peg-game-page-project", new PagesProjectArgs
    {
        AccountId = accountId,
        Name = pagesProjectName,
        ProductionBranch = "main",
    });

    var pagesDomain = new PagesDomain("triangle-peg-game-pages-domain", new PagesDomainArgs
    {
        AccountId = accountId,
        ProjectName = pagesProject.Name,
        Domain = $"{subdomain}.{domain}",
    });
    
    var zoneSettings = new ZoneSettingsOverride("triangle-peg-game-zone-settings", new ZoneSettingsOverrideArgs
    {
        ZoneId = zone.Apply(z => z.Id),
        Settings = new ZoneSettingsOverrideSettingsArgs
        {
            Minify = new ZoneSettingsOverrideSettingsMinifyArgs
            {
                Css = "off",
                Html = "off",
                Js = "off"
            },
            RocketLoader = "off",
            AlwaysUseHttps = "on"
        }
    });

    var objects = new[]
    {
        // budget.Create(),
        new Dictionary<string, object?>
        {
            ["pagesUrl"] = pagesProject.Subdomain.Apply(s => $"https://{s}.pages.dev"),
            ["customDomainUrl"] = Output.Create($"https://{subdomain}.{domain}"),
            ["pagesProjectName"] = pagesProject.Name,
            ["accountId"] = accountId
        }
    };

    return objects.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
});