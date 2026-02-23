using TrianglePegGameSolver.Web.Application;

namespace TrianglePegGameSolver.Web.Tests.Shared;

public class ApplicationFixture : MediatorFixture
{
    public ApplicationFixture()
    {
        OnConfigureServices += (_, services) =>
        {
            services.AddApplication();
        };
    }
}
