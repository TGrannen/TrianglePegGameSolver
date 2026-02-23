using Fluxor;

namespace TrianglePegGameSolver.Web.Features.Navigation.Store;

public record NavigationState
{
    public bool Open { get; init; }
}


public class PlayGameFeature : Feature<NavigationState>
{
    public override string GetName() => "Navigation";

    protected override NavigationState GetInitialState()
    {
        return new NavigationState
        {
            Open = false
        };
    }
}