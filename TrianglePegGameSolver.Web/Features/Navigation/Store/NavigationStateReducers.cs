using Fluxor;

namespace TrianglePegGameSolver.Web.Features.Navigation.Store
{
    public static class NavigationStateReducers
    {
        [ReducerMethod]
        public static NavigationState MoveMadeAction(NavigationState state, NavigationAction action)
        {
            return state with
            {
                Open = action.Open
            };
        }
    }
}