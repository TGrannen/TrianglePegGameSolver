using Pulumi.AzureNative.Authorization;

namespace TrianglePegGameSolver.Cloud.Infrastructure.Helpers;

public class StackConfig
{
    public readonly Config PulumiConfig = new();

    public StackConfig()
    {
        RepoName = PulumiConfig.Require("repo-name");
        Token = PulumiConfig.Require("repo-token");
        Domain = PulumiConfig.Require("domain");
        Env = PulumiConfig.Require("env");
        APILocation = PulumiConfig.Get("api-location");
        NameBase = $"triangle-peg-game-{Env}";
    }

    public string RepoName { get; }
    public string Token { get; }
    public string Domain { get; }
    public string Env { get; }
    public string? APILocation { get; }
    public string? NameBase { get; }
    public Output<string> ResourceGroupName { get; set; }
    public GetClientConfigResult ClientConfig { get; set; }
}